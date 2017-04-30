using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using PatternTools;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace GO
{
    public partial class GOControl : UserControl
    {
        GOTerms gt = new GOTerms();
        ImageList imageList = new ImageList();
        List<TreeNode> treeNodes = new List<TreeNode>();
        List<string> frontLine = new List<string>();  //This array will keep track of the frontline in the tree
        bool dagLoaded;
        bool associationTableloaded;
        WaitWindow waitWindow;
        TermScoreCalculator termScoreCalculator;  // used to calculate fold changes and the hypegeo
        ResultParser resultParser = new ResultParser(); //Used for loading differential expression data
        ////This is a container for the evaluated terms
        static List<TermScoreCalculator.TermScoreAnalysis> termScoreAnalysisListForAllParameters = new List<TermScoreCalculator.TermScoreAnalysis>(25000);//The container used in the search all terms method

        public GOControl()
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();

            imageList.Images.Add((System.Drawing.Image)Properties.Resources.blue);
            imageList.Images.Add((System.Drawing.Image)Properties.Resources.red );
            imageList.Images.Add((System.Drawing.Image)Properties.Resources.yellow);
            imageList.Images.Add((System.Drawing.Image)Properties.Resources.green);
            imageList.Images.Add((System.Drawing.Image)Properties.Resources.equalsImage);    
            
            treeViewGO2.ImageList = imageList;

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Indicate the GO (DAG) file";
            openFileDialog1.Filter = "OBO file (*.OBO)|*.OBO";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }

            gt.LoadFile(openFileDialog1.FileName);
            richTextBoxLog.AppendText("Nodes loaded :" + gt.Terms.Count.ToString() + "\n");
            
            int prunnedNodes = gt.pruneObsoleteTerms();
            richTextBoxLog.AppendText("Prunned nodes (obsolete): " + prunnedNodes.ToString() + "\n");
            richTextBoxLog.AppendText("We now have " + gt.Terms.Count.ToString() + "nodes\n");
            
            gt.buildTreeNodes();

            //Lets give them a friendly wait advice
            waitWindow = new WaitWindow();
            waitWindow.ChangeLable("Performing Optimizations...");
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(waitWindow.ShowWindow));
            t.Start();

            tabControlGOTools.Enabled = true;
            tabControlRight.Enabled = true;

            treeViewGO2.ShowPlusMinus = false;

            treeNodes.Clear();
            treeNodes = gt.getNodesCategory(GO.GOTerms.GOCategories.all);

            addRoots();

            //Perform Precalculations
            //We could let this computing in a sepatared thread
            gt.ComputeAllNodesBelowForAllNodes();
            
            dagLoaded = true;

            if (dagLoaded && associationTableloaded)
            {
                buttonSavePreCalc.Enabled = true;
                groupBoxTreeControls.Enabled = true;
                groupBoxGOPlot.Enabled = true;
            }
            openFileDialog1.Filter = "";

            //Turn off the button so the user wont fo mess
            buttonLoad.Enabled = false;
            groupBoxGOPrecomp.Enabled = false;

            t.Abort();
        }

        private void buttonTracePath_Click(object sender, EventArgs e)
        {
            tabControlRight.SelectedTab = tabPageLog;
            
            List<List<string>> routes = gt.AllRoutesToRootsFrom(textBoxFetch.Text);

            richTextBoxLog.AppendText("Tracing route from "+ textBoxFetch.Text + "\n");

            foreach (List<string> l in routes)
            {
                richTextBoxLog.AppendText( "Route : " + routes.IndexOf(l).ToString() + "\n" );
                foreach (string s in l)
                {
                    richTextBoxLog.AppendText(s + "\n");
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
        }


        /// <summary>
        /// This method is limited to prune leafes
        /// </summary>
        /// <param name="theTree"></param>
        /// <param name="theLeafes"></param>
        /// <returns></returns>


        //-----------------------------------

        private void buttonLoadAssociationTable2Ram_Click(object sender, EventArgs e)
        {
            ChooseParsing cp = new ChooseParsing();
            cp.ShowDialog();

            if (cp.doWork == false) { return; }

            //try
            //{
                //Lets give them a friendly wait advice
                waitWindow = new WaitWindow();
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(waitWindow.ShowWindow));
                t.Start();

                //Nee to put this or else it will crash for small files
                Thread.Sleep(500);

                gt.LoadAssociationTable(cp.pathTOGoa, cp.pathTOIPIXRefsFilename, cp.AssociationFileType, cp.GOColumn, cp.myColumn);
                richTextBoxLog.AppendText("Associations Loaded to RAM.\n");

                associationTableloaded = true;

                if (dagLoaded && associationTableloaded)
                {
                    buttonSavePreCalc.Enabled = true;
                    groupBoxTreeControls.Enabled = true;
                    groupBoxGOPlot.Enabled = true;
                }

                t.Abort();
                buttonLoadAssociationTable2Ram.Enabled = false;
                groupBoxGOPrecomp.Enabled = false;
            //}
            //catch (Exception ee)
            //{
            //    PatternTools.Forms.Message mf = new PatternTools.Forms.Message(ee.Message);
            //    mf.ShowDialog();
            //}

        }

        private void addRoots ()
        {
            treeViewGO2.CollapseAll();
            treeViewGO2.Nodes.Clear();
            frontLine.Clear();

            List<string> rootNames = gt.findTopMostNodes();            

            foreach (TreeNode tn in treeNodes)
            {
                if (rootNames.Contains(tn.Name)) {
                    if (!treeViewGO2.Nodes.Contains(tn))
                    {
                        treeViewGO2.Nodes.Add(cleanNode(tn));
                        frontLine.Add(tn.FullPath);
                    }
                }
            }

        }

        private static TreeNode cleanNode(TreeNode n)
        {
            n.ImageIndex = 3;
            n.Nodes.RemoveAt(0);
            n.Nodes.RemoveAt(0);
            n.Nodes.RemoveAt(1);
            n.Nodes[0].ImageIndex = 4;

            return (n);
        }


        private void buttonExpand_Click(object sender, EventArgs e)
        {
            TreeNode sNode = new TreeNode();
            int counter = 0;

            //we already exanded it!
            if (treeViewGO2.SelectedNode.Nodes.Count > 1)
            {
                //now, remove the children from the node

                int noOfChildren = treeViewGO2.SelectedNode.Nodes.Count;
                for (int i = noOfChildren - 1; i > 0; i--)
                {
                    treeViewGO2.SelectedNode.Nodes.RemoveAt(i);
                }
                return;
            }

            try
            {
                sNode = treeViewGO2.SelectedNode;

                if (sNode.Nodes.Count > 1)
                {
                    richTextBoxLog.AppendText("You already expanded that node.\n");
                }
                else
                {

                    //Rake the treenodes searching for any node that belongs to the selected node
                    foreach (TreeNode tn in treeNodes)
                    {
                        foreach (TreeNode parentName in tn.Nodes[0].Nodes)
                        {
                            if (sNode.Name.Equals(parentName.Name))
                            {
                                if (!sNode.Nodes.Contains(tn))
                                {
                                    counter++;
                                    TreeNode tc = (TreeNode)tn.Clone();
                                    sNode.Nodes.Add(cleanNode(tc));
                                }
                            }
                        }

                    }

                    if (sNode.Nodes.Count > 1)
                    {
                        sNode.ImageIndex = 2; //yellow
                    }
                    else
                    {
                        sNode.ImageIndex = 1; //red
                    }

                    //We should always skipp node zero since he is the description
                    for (int i = 1; i < treeViewGO2.SelectedNode.Nodes.Count; i++)
                    {
                        frontLine.Add(treeViewGO2.SelectedNode.Nodes[i].FullPath);
                    }

                    //If the node is a leaf, we need to add him
                    if (treeViewGO2.SelectedNode.Nodes.Count == 1 && treeViewGO2.SelectedNode.SelectedImageIndex != 1)
                    {
                        //frontLine.Add(treeViewGO2.SelectedNode.FullPath);
                    }

                    
                    //Bug correction added 03.06.09, remove any frontline terms that have been twicely considered
                    List<string> flCorrected = new List<string>();
                    foreach (string s in frontLine)
                    {
                        flCorrected.Add(s);
                    }
                    frontLine.Clear();
                    Regex slashsplitter = new Regex(@"\\");
                    List<string> includedTerms = new List<string>();

                    foreach (string s in flCorrected)
                    {
                        string[] theLastTerm = slashsplitter.Split(s);
                        if (!includedTerms.Contains(theLastTerm[theLastTerm.Length-1]))
                        {
                            frontLine.Add(s);
                            includedTerms.Add(theLastTerm[theLastTerm.Length-1]);
                        }
                    }

                    //End bug correction

                    richTextBoxLog.AppendText(counter.ToString() + "nodes added.\n");
                }

                //Now we force the tree to expand the node
                sNode.Expand();
            }
            catch
            {
                MessageBox.Show("Please select a node to expand.");
                return;
            }
            
            
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            
            treeNodes.Clear();
            gt.buildTreeNodes();
            treeNodes = gt.getNodesCategory(GO.GOTerms.GOCategories.all);
            
            addRoots();

            richTextBoxLog.AppendText("Tree reset!");
        }

       

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                treeViewGO2.SelectedNode.Collapse(false);
                frontLine.Remove(treeViewGO2.SelectedNode.FullPath);
                treeViewGO2.SelectedNode.Remove();
            }
            catch
            {
                MessageBox.Show("Please select node!\n");
            }
        }

        private void NodeCollapsing(object sender, TreeViewEventArgs e)
        {
            //we should keep track of the nodes that are open

            //Turn The Node Back green
            e.Node.ImageIndex = 3;
            e.Node.Collapse(false);

            //Remove his children from the frontline
            for (int i = 1; i < e.Node.Nodes.Count; i++)
            {
                frontLine.Remove(e.Node.Nodes[i].FullPath);
            }

            //Add him back to the frontline only if the front line
            //does not have him yet

            if (!frontLine.Contains(e.Node.FullPath)) {
                frontLine.Add(e.Node.FullPath);
            }
        }

        private void NodeExpanding(object sender, TreeViewEventArgs e)
        {
            //we should keep track of the nodes that are expanding

            //remove him from the front line and add his children
            frontLine.Remove(e.Node.FullPath);

            //Make sure the first child has the equals sign
            e.Node.Nodes[0].ImageIndex = 4;
        }

        private void buttonPrintFrontLine_Click(object sender, EventArgs e)
        {
            richTextBoxLog.AppendText("\nFrontLine\n");

            foreach (string s in frontLine)
            {
                richTextBoxLog.AppendText(s + "\n");
            }
        }

        private void buttonStudyAGoTerm_Click(object sender, EventArgs e)
        {
            //Check if we have the results already loaded
            try
            {
                checkIfResultsAreLoaded();
            }
            catch (InvalidOperationException)
            {
                return;
            }
            catch (Exception e2)
            {
                MessageBox.Show("Please verify the result report\n" + e2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Term theTerm = gt.Terms.Find(a => a.id.Equals(textBoxFetch.Text));

            if (theTerm != null)
            {
                richTextBoxLog.AppendText("Term Fetched: " + textBoxFetch.Text + "\n");
                richTextBoxLog.AppendText("Term Name: " + theTerm.name + "\n");
                richTextBoxLog.AppendText("Term Definition: " + theTerm.definition + "\n");


                GO.TermScoreCalculator.TermScoreAnalysis tsa = termScoreCalculator.calculate(theTerm, false, true);

                richTextBoxLog.AppendText("Proteins mapped to this Go Term: " + tsa.ProteinIDs.Keys.Count + "\n");

                foreach (KeyValuePair<string, string> kvp in tsa.ProteinIDs)
                {
                    richTextBoxLog.AppendText(kvp.Key + "\t" + kvp.Value + "\n");
                }

                richTextBoxLog.AppendText("\n");

            }
            else
            {
                richTextBoxLog.AppendText("Term not found.");
            }

        }

        private void buttonFetch_Click(object sender, EventArgs e)
        {
            tabControlRight.SelectedTab = tabPageLog; 

            List<string> translations = gt.AssociationTranslator.Translate(textBoxFetch.Text);
            List<Term> theTerms = gt.Terms;
            List<Term> leafs = gt.FindLeafes(ref theTerms);

            //We want to print besides the term if it is a leaf
            foreach (string s in translations)
            {
                bool isLeaf = false;

                //We need to remove the GO:
                string translatedCorrected = System.Text.RegularExpressions.Regex.Replace(s, "GO:", "");
                Term mtTerm = gt.getTermByID(translatedCorrected);
                
                //Is it a leaf?
                foreach (Term t in leafs)
                {
                    if (s.EndsWith(t.id))
                    {
                        isLeaf = true;
                    }
                }

                richTextBoxLog.AppendText(mtTerm.id + " " + mtTerm.name + " " + "Is Leaf: "+ isLeaf + "("+ mtTerm.nameSpace +") "+ mtTerm.definition +"\n\n");
            }
        }

        public void buttonRoutsToLeafs_Click(object sender, EventArgs e)
        {
            tabControlRight.SelectedTab = tabPageLog;
            
            //Be careful not to call this method more than once!!!
            List<List<string>> routes = gt.allRoutesToLeafsFrom(textBoxFetch.Text);

            richTextBoxLog.AppendText("Tracing route to leaf from " + textBoxFetch.Text + "\n");

            foreach (List<string> l in routes)
            {
                richTextBoxLog.AppendText("Route : " + routes.IndexOf(l).ToString() + "\n");
                foreach (string s in l)
                {
                    richTextBoxLog.AppendText(s + "\n");
                }
            }
        }

        private void buttonAllTheNodesBelow_Click(object sender, EventArgs e)
        {
            tabControlRight.SelectedTab = tabPageLog;
            
            Term l = gt.getTermByID(textBoxFetch.Text);

            richTextBoxLog.AppendText("Nodes below : "+l.AllNodesBelow.Count.ToString() + "\n");
            
            //foreach (string s in l.AllNodesBelow)
            //{
                //richTextBox1.AppendText(s + "\n");
            //}
            
            richTextBoxLog.AppendText("\n");
        }

        private void buttonPrintAllNodesBelow_Click(object sender, EventArgs e)
        {
            tabControlRight.SelectedTab = tabPageLog;

            Term theTerm = gt.getTermByID(textBoxFetch.Text);

            richTextBoxLog.AppendText("There are "+ theTerm.AllNodesBelow.Count + " nodes below\n");
            
            //foreach (string s in theTerm.AllNodesBelow)
            //{
            //    richTextBox1.AppendText(s + "\n");
            //}
        }

        private void buttonComputeAllNodesBelowAll_Click(object sender, EventArgs e)
        {
            richTextBoxLog.AppendText("Began Computing\n");
            gt.ComputeAllNodesBelowForAllNodes();
            richTextBoxLog.AppendText("Finished!\n");
        }


        private void buttonSavePreCalc_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "GO Precomputed (*.GOprecomp)|*.GOprecomp";
            saveFileDialog1.ShowDialog();

            System.IO.FileStream flStream = new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(flStream, gt);
            flStream.Close();
            richTextBoxLog.AppendText("GO Serialized!\n");
            saveFileDialog1.Filter = "";
            buttonSavePreCalc.Enabled = false;

        }

        private void buttonLoadPreCalc_Click(object sender, EventArgs e)
        {
            waitWindow = new WaitWindow();
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(waitWindow.ShowWindow));

            try
            {
                openFileDialog1.Filter = "GO Precomputed (*.GOprecomp)|*.GOprecomp";
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }
                openFileDialog1.Filter = "";

                //Lets give them a friendly wait advice
                t.Start();
                System.IO.FileStream flStream = new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                this.gt = (GOTerms)bf.Deserialize(flStream);
                flStream.Close();
                richTextBoxLog.AppendText("Object deserialized!\n");


                gt.buildTreeNodes();
                tabControlGOTools.Enabled = true;

                treeViewGO2.ShowPlusMinus = false;
                tabControlRight.Enabled = true;

                treeNodes.Clear();
                treeNodes = gt.getNodesCategory(GO.GOTerms.GOCategories.all);

                addRoots();

                //Disable some stuff to keep the user on track
                buttonLoadPreCalc.Enabled = false;
                groupBoxLoadGODAG.Enabled = false;

                groupBoxTreeControls.Enabled = true;
                groupBoxGOPlot.Enabled = true;
            }
            catch (Exception e2)
            {
                richTextBoxLog.AppendText(e2.InnerException + "\n");
            }
            finally
            {
                //Lets shut down our wait window
                t.Abort();
            }

        }

        private void buttonVerifyExperimentalData_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }

            ResultParser resultParser = new ResultParser();
            resultParser.ParseTrendQuestACFoldOrIndex(openFileDialog1.FileName);

            int failCounter = 0;
            int idsProcessedCounter = 0;
            List<string> failedIDs = new List<string>();

            foreach (KeyValuePair<string, double> r in resultParser.TheResults)
            {
                try
                {
                    idsProcessedCounter++;
                }
                catch
                {
                    failCounter++;
                    failedIDs.Add(r.Key);
                }
            }

            richTextBoxLog.AppendText("Total IDs processed =" + idsProcessedCounter.ToString() + "\n");
            richTextBoxLog.AppendText("FailCounter = " + failCounter.ToString() + "\n");

            for (int i = 0; i < failedIDs.Count; i++)
            {
                int a = i + 1;
                richTextBoxLog.AppendText(a.ToString() + ": " + failedIDs[i] + "\n");
            }

        }

        private void buttonCalculateHypeGeoForATerm_Click(object sender, EventArgs e)
        {
            tabControlRight.SelectedTab = tabPageLog;
            //Check if we have the results already loaded
            try
            {
                checkIfResultsAreLoaded();
            }
            catch
            {
                MessageBox.Show("Please verify the result report", "Error");
                return;
            }

            TermScoreCalculator.TermScoreAnalysis tsa = termScoreCalculator.calculate(gt.getTermByID(textBoxFetch.Text), checkBoxLeafs.Checked, true);
            
            //Give some output for the user
            richTextBoxLog.AppendText("ID: "+ tsa.Term + "\n");
            richTextBoxLog.AppendText("Population size (leafs in the namespace) :"+ tsa.PopulationSize.ToString() + "\n");
            richTextBoxLog.AppendText("Study set size (leafs in term) :" + tsa.StudySetSize.ToString() + "\n");
            richTextBoxLog.AppendText("Identified terms in population :" + tsa.IdentifiedTermsInPopulation.ToString() + "\n");
            richTextBoxLog.AppendText("Identified terms in study set :" + tsa.IdentifiedTermsInStudySet.ToString() + "\n");
            richTextBoxLog.AppendText("Cum. HypeGeo Prob :" + Math.Round(tsa.CummulativeHypeGeo, 4).ToString()+ "\n\n");
            richTextBoxLog.AppendText("ABS fold change :" + Math.Round(tsa.AbsoluteFoldChange, 2).ToString()+ "\n");
            richTextBoxLog.AppendText("Fold change :" + Math.Round(tsa.FoldChange, 2).ToString() + "\n");
            
        }

        private void buttonPrintLeafs_Click(object sender, EventArgs e)
        {
            tabControlRight.SelectedTab = tabPageLog;
            
            List<string> leafs = gt.GetLeafs(gt.getTermByID(textBoxFetch.Text));

            richTextBoxLog.AppendText("Leafs below " + textBoxFetch.Text + "\n");
            richTextBoxLog.AppendText(leafs.Count.ToString() + "\n");

        }

        private void PlotPieDotAndUpdateTable(List<TermScoreCalculator.TermScoreAnalysis> termScoreAnalysisList, int minimumDepth)
        {
            //And the pie chart
            chartPieNew.Series[0].Points.Clear();
            //Clear the datatable
            dataGridViewGraphData.Rows.Clear();
            

            List<PieChartItem> pci = new List<PieChartItem>();

            foreach (var tsa in termScoreAnalysisList)
            {
                //Lets clean it from the treeview
                if (tsa.AbsoluteFoldChange == 0)
                {
                    TreeNode[] nodesToClear = treeViewGO2.Nodes.Find(tsa.Term, true);
                    foreach (TreeNode n in nodesToClear)
                    {
                        n.Remove();
                    }
                }

                //We will compute it if it has a 0 fold change?
                if (tsa.AbsoluteFoldChange == 0)
                {
                    continue;
                }

                string tip = tsa.TermName + " (" + tsa.Term + ")\n";
                tip += tsa.Namespace + "\n";
                tip += tsa.IdentifiedTermsInStudySet + "/" + tsa.StudySetSize + " in " + tsa.IdentifiedTermsInPopulation + "/" + tsa.PopulationSize + "\n";
                tip += tsa.Description;


                //newPie
                pci.Add(new PieChartItem((double)tsa.IdentifiedTermsInStudySet, tsa.TermName));

                //Plot the column wpf chart
                barControl1.Plot(termScoreAnalysisList, resultParser.MyPatternLabProject, (int)numericUpDownFontSize.Value);

                //Calculate Shortest Route to root
                List<List<string>> allRoutes = gt.AllRoutesToRootsFrom(tsa.Term);
                int shortestRoute = int.MaxValue;
                foreach (List<string> route in allRoutes)
                {
                    if (route.Count < shortestRoute)
                    {
                        shortestRoute = route.Count;
                    }
                }

                if (shortestRoute < minimumDepth) { continue; }


                //Update the data table
                int i = dataGridViewGraphData.Rows.Add();
                dataGridViewGraphData.Rows[i].Cells[0].Value = tsa.Term;
                dataGridViewGraphData.Rows[i].Cells[1].Value = tsa.TermName;
                dataGridViewGraphData.Rows[i].Cells[2].Value = tsa.Namespace;
                dataGridViewGraphData.Rows[i].Cells[3].Value = Math.Round(tsa.AbsoluteFoldChange, 2);
                dataGridViewGraphData.Rows[i].Cells[4].Value = Math.Round(tsa.FoldChange, 2);
                dataGridViewGraphData.Rows[i].Cells[5].Value = tsa.CummulativeHypeGeo;
                dataGridViewGraphData.Rows[i].Cells[6].Value = tsa.StudySetSize;
                dataGridViewGraphData.Rows[i].Cells[7].Value = tsa.PopulationSize;
                dataGridViewGraphData.Rows[i].Cells[8].Value = tsa.IdentifiedTermsInStudySet;
                dataGridViewGraphData.Rows[i].Cells[9].Value = shortestRoute;
                dataGridViewGraphData.Rows[i].Cells[10].Value = tsa.ProteinIDsAndFold;
                dataGridViewGraphData.Rows[i].Cells[11].Value = tsa.Description;


            }


            //Prepare the pie chart--------------------------------------
            pci.Sort((a, b) => b.Count.CompareTo(a.Count));

            List<PieChartItem> correctedPCI = new List<PieChartItem>();

            PieChartItem pciOther = new PieChartItem(0, "Others");
            for (int i = 0; i < pci.Count; i++)
            {
                if (i < numericUpDownNumberOfSlicesInPie.Value)
                {
                    correctedPCI.Add(pci[i]);

                }
                else
                {
                    pciOther.Count += pci[i].Count;
                }
            }

            if (pciOther.Count > 0)
            {
                correctedPCI.Add(pciOther);
            }

            chartPieNew.Series[0].Font = new Font("Microsoft Sans Serif", (float)numericUpDownFontSize.Value);
            chartPieNew.Series[0]["PieDrawingStyle"] = "Concave";
            chartPieNew.Series[0]["PieLabelStyle"] = "Outside";

            double total = correctedPCI.Sum(a => a.Count);
            foreach (PieChartItem t in correctedPCI)
            {
                double percent = Math.Round((t.Count / total) * 100 , 1);
                chartPieNew.Series[0].Points.AddXY(t.TermName + " " + percent + "%", t.Count);
            }
            //-------------------------------------------------------

        }


        /// <summary>
        /// This method should alwys be called in a try catch
        /// </summary>
        private void checkIfResultsAreLoaded()
        {
            if (resultParser.TheResults.Count == 0)
            {
                openFileDialog1.Title = "Load results";
                openFileDialog1.Filter = "PatternLab project file (*.plp)|*.plp|AC/TFold, TrendQuest files (*.txt)|*.txt";

                if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    resultParser.ParseTrendQuestACFoldOrIndex(openFileDialog1.FileName);
                    resultParser.PrepareTranslatedDictionary(ref gt);

                    richTextBoxLog.AppendText("User expression data loaded\n");
                    termScoreCalculator = new TermScoreCalculator(ref gt, ref resultParser);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private void buttonKeyWordSearch_Click(object sender, EventArgs e)
        {
            //kryptonNavigatorGraph.SelectedPage = kryptonPageLog;

            //Check if we have the results already loaded
            try
            {
                checkIfResultsAreLoaded();
            }
            catch
            {
                MessageBox.Show("Please verify the result report", "Error");
                return;
            }

            //Now lets get these terms!
            string[] words = textBoxKeyWordsSearch.Text.Split(" ".ToCharArray());

            richTextBoxLog.AppendText("Searching the GO terms having the following keywords:");
            foreach (string word in words)
            {
                richTextBoxLog.AppendText(word + "\n");
            }

            List<TermScoreCalculator.TermScoreAnalysis> tsa = new List<TermScoreCalculator.TermScoreAnalysis>();

            richTextBoxLog.AppendText("\nSearch results:\n");

            
            foreach (Term t in gt.Terms)
            {
                int wordsFound = 0;
                if (t.definition == null) { continue; }
                foreach (string word in words)
                {
                    Regex keyWordRegex = new Regex(word);

                    //if (t.name.Contains(word) || t.definition.Contains(word))
                    if (keyWordRegex.IsMatch(t.name) || keyWordRegex.IsMatch(t.definition))
                    {
                        wordsFound++;
                    }
                }
                if (wordsFound == words.Length)
                {
                    //Add the term to plot it later
                    tsa.Add(termScoreCalculator.calculate(t, checkBoxLeafs.Checked, false));

                    //Put some infor in the log
                    richTextBoxLog.AppendText("Term found :" + t.id + "\n");
                    richTextBoxLog.AppendText("ID: " + t.name + "\n");
                    richTextBoxLog.AppendText("ID space :" + t.nameSpace + "\n");
                    richTextBoxLog.AppendText("Definiton :" + t.definition + "\n");

                    //Find at least one path to the root term
                    List<string> ids = gt.findOneRoutToRoot(t.id);
                    foreach (string id in ids)
                    {
                        Term t2 = gt.getTermByID(id);
                        richTextBoxLog.AppendText("->"+t2.name+"("+t2.id+")");
                    }
                    richTextBoxLog.AppendText("\n\n");
                }
            }

            //Plot the terms
            PlotPieDotAndUpdateTable(tsa, 0);
        }


        private void buttonEvaluateMappingToGO_Click(object sender, EventArgs e)
        {
            try
            {
                checkIfResultsAreLoaded();
            }
            catch (Exception e2)
            {
                MessageBox.Show("Please verify the result report\n" + e2.Message, "Error");
                return;
            }

            Console.WriteLine("Total proteins loaded = " + resultParser.MyProteins.Count);

            List<GO.ResultParser.ProteinInfo> translatedProteins = resultParser.MyProteins.FindAll(a => gt.AssociationTranslator.Translate(a.ID).Count > 0);

            Console.WriteLine(translatedProteins.Count + " proteins were translated.");
            richTextBoxLog.AppendText(translatedProteins.Count + " proteins were translated.");

        }


        private void linkLabelToAssociations_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MyWebBrowser mwb = new MyWebBrowser();
            mwb.SetURL("http://www.ebi.ac.uk/GOA/goaHelp.html");
            mwb.ShowDialog();
        }

        private void linkLabelToDAG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MyWebBrowser mwb = new MyWebBrowser();
            mwb.SetURL("http://www.geneontology.org/GO.downloads.ontology.shtml");
            mwb.ShowDialog();
        }

        private void buttonLoadNewReport_Click(object sender, EventArgs e)
        {
            //Reset everybody
            resultParser.TheResults.Clear();
            if (termScoreCalculator != null)
            {
                termScoreCalculator.ClearTermScoreAnalysisCache();
            }
            if (termScoreAnalysisListForAllParameters != null)
            {
                termScoreAnalysisListForAllParameters.Clear();
            }


            openFileDialog1.Title = "Load result file.";
            openFileDialog1.Filter = "Text file (*.txt)|*.txt";

            openFileDialog1.ShowDialog();

            resultParser.ParseTrendQuestACFoldOrIndex(openFileDialog1.FileName);
            resultParser.PrepareTranslatedDictionary(ref gt);

            richTextBoxLog.AppendText("User expression data loaded\n");
            termScoreCalculator = new TermScoreCalculator(ref gt, ref resultParser);
        }


        private void buttonSearchAll_Click(object sender, EventArgs e)
        {
            //Check if we have the results already loaded
            try
            {
                checkIfResultsAreLoaded();
            }
            catch
            {
                MessageBox.Show("Please verify the result report", "Error");
                return;
            }

            //Collect the search all parameters
            SearchAll searchAllWindow = new SearchAll();
            searchAllWindow.ShowDialog();

            //Make sure he pressed the work button
            if (!searchAllWindow.Work) { return; }

            //Now lets rock!
            double hypeGeoPValueCutoff = searchAllWindow.HyperGeometricPValueCutOff;
            int minimumNumberOfTerms = searchAllWindow.MinimumNumberOfTerms;
            bool useFDR = searchAllWindow.FDR;
            double FDRAlfa = searchAllWindow.FDRAlfa;
            int minimumDepth = searchAllWindow.MinimumDepth;

            //Give them the wait window
            WaitWindow ww = new WaitWindow();
            System.Threading.Thread tt = new System.Threading.Thread(new System.Threading.ThreadStart(ww.ShowWindow));
            tt.Start();

            //Analyze all terms
            if (termScoreAnalysisListForAllParameters.Count == 0)
            {
                ww.ChangeLowerLable("Please wait... evaluating for " + gt.Terms.Count.ToString() + " terms.");

                termScoreAnalysisListForAllParameters = (from toEvaluate in gt.Terms.AsParallel()
                                                         select termScoreCalculator.calculate(toEvaluate, false, true)).ToList();

            }

            //Performing post processing;
            //sort the arrays
            if (useFDR)
            {
                //Sorts ascending by pvalue then by foldChange
                termScoreAnalysisListForAllParameters.Sort((a, b) => a.FoldChange.CompareTo(b.FoldChange));
                termScoreAnalysisListForAllParameters.Sort((a, b) => a.CummulativeHypeGeo.CompareTo(b.CummulativeHypeGeo));
            }

            //Prepare the array for plotting
            List<TermScoreCalculator.TermScoreAnalysis> termToPlot = new List<TermScoreCalculator.TermScoreAnalysis>();
            int counter2 = 0;
            foreach (TermScoreCalculator.TermScoreAnalysis tsc in termScoreAnalysisListForAllParameters)
            {
                counter2++;
                if (tsc.CummulativeHypeGeo <= hypeGeoPValueCutoff && tsc.AbsoluteFoldChange > 1 && tsc.IdentifiedTermsInStudySet >= minimumNumberOfTerms)
                {
                    if (useFDR)
                    {
                        double FDRCutoff = (FDRAlfa * counter2) / termScoreAnalysisListForAllParameters.Count;
                        if (tsc.CummulativeHypeGeo <= FDRCutoff)
                        {
                            termToPlot.Add(tsc);
                        }
                    }
                    else
                    {
                        termToPlot.Add(tsc);
                    }
                }
            }

            //Plotting
            PlotPieDotAndUpdateTable(termToPlot, minimumDepth);

            //Shut down our wait window
            tt.Abort();
        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            Plot();
        }

        private void Plot()
        {
            //Check if we have the results already loaded
            try
            {
                checkIfResultsAreLoaded();
            }
            catch (InvalidOperationException)
            {
                return;
            } 
            catch (Exception e2)
            {
                MessageBox.Show("Please verify the result report\n" + e2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            buttonPlot.Text = "Working!";
            this.Update();

            List<Term> termsToBeEvaluated = new List<Term>(frontLine.Count);
            List<TermScoreCalculator.TermScoreAnalysis> termScoreAnalysisList = new List<TermScoreCalculator.TermScoreAnalysis>();

            //Write the frontLine in the log
            richTextBoxLog.AppendText("Terms evaluated:\n");

            Regex goIDMatch = new Regex(@"(?<cavMatch>[0-9]{7})");

            //We need to get only the last GO of the path
            for (int i = 0; i < frontLine.Count; i++)
            {
                richTextBoxLog.AppendText(i.ToString() + "\t" + frontLine[i] + "\n");

                //We need to clean the frontline string
                MatchCollection mc = goIDMatch.Matches(frontLine[i]);
                string parsedIDTerm = mc[mc.Count - 1].Value;
                termsToBeEvaluated.Add(gt.getTermByID(parsedIDTerm));
            }

            foreach (var categoryToBeEvaluated in termsToBeEvaluated)
            //Parallel.ForEach(termsToBeEvaluated, categoryToBeEvaluated =>
            {
                termScoreAnalysisList.Add(termScoreCalculator.calculate(gt.getTermByID(categoryToBeEvaluated.id), checkBoxLeafs.Checked, false));
            }


            //Finally, lets get to drawing the chart
            PlotPieDotAndUpdateTable(termScoreAnalysisList, 0);
            buttonPlot.Text = "Plot";
        }



        private void buttonTest_Click(object sender, EventArgs e)
        {
            //splitContainerMainControls.Panel1.Controls.Add(splitContainer3);
            splitContainerMainControls.Panel2.Controls.Add(tabControlRight);
            //splitContainerMainControls.Panel2.Controls.Add(splitContainer3);
            //splitContainer2.Panel1.Controls.Add(this.groupBoxTreeControls);


        }

        private void numericUpDownNumberOfSlicesInPie_ValueChanged(object sender, EventArgs e)
        {
            Plot();
        }

        private void buttonSavePie_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG files (*.png)|*.png";
            saveFileDialog1.FileName = "PieChart";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {

                ChartImageFormat format = ChartImageFormat.Png;
                chartPieNew.SaveImage(saveFileDialog1.FileName, format);
            }
        }

        private void buttonSaveGraphData_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
            saveFileDialog1.FileName = "ExperimentalData";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) { return; }
            Regex cleanProteinRegex = new Regex(@"\([0-9]+\)");
            try
            {
                System.IO.StreamWriter WRITE = new System.IO.StreamWriter(saveFileDialog1.FileName);

                WRITE.WriteLine("#Gene Ontology Report (GOEx-PatternLab)");
                WRITE.WriteLine("# [0-termID] [1-termName] [2-nameSpace] [3-absoluteFoldChange] [4-foldChange] [5-HypeGeo] [6-studyset] [7-population] [8-Matched in studyset] [9-Depth] [10-Description], Matched in population listed below\n");
                WRITE.WriteLine("");

                for (int r = 0; r < dataGridViewGraphData.Rows.Count; r++)
                {

                    List<string> theCells = new List<string>();

                    for (int c = 0; c < dataGridViewGraphData.Rows[r].Cells.Count; c++)
                    {

                        string cellValue = dataGridViewGraphData.Rows[r].Cells[c].Value.ToString();
                        //remove any \n
                        string modified = System.Text.RegularExpressions.Regex.Replace(cellValue, "\n", "");
                        modified = Regex.Replace(modified, "\t", "");
                        theCells.Add(modified);
                    }

                    string thePtns = "";

                    for (int i = 0; i < theCells.Count; i++)
                    {
                        if (i == 10)
                        {
                            //Write the proteins, one per line
                            thePtns = dataGridViewGraphData.Rows[r].Cells[i].Value.ToString();
                            string[] thePtnsArray = Regex.Split(thePtns, " ");



                            foreach (string ptn in thePtnsArray)
                            {
                                if (ptn.Equals("")) { continue; }
                                //We need to remove the quantitation data (number)
                                try
                                {
                                    string cleanProtein = cleanProteinRegex.Replace(ptn, "");
                                    GO.ResultParser.ProteinInfo pi = resultParser.MyProteins.Find(a => a.ID.Equals(cleanProtein));
                                    WRITE.WriteLine("\t" + ptn + "\t" + pi.Description);
                                    WRITE.Flush();
                                }
                                catch
                                {
                                    WRITE.WriteLine("\t" + ptn + "\tDescription not found");
                                    WRITE.Flush();
                                }

                            }
                        }
                        else
                        {
                            if (i != 9)
                            {
                                WRITE.Write(theCells[i] + "\t");
                            }
                            else
                            {
                                WRITE.WriteLine(theCells[i]);
                            }
                            WRITE.Flush();
                        }
                    }

                    WRITE.WriteLine("");

                    //Write the Proteins below
                    string[] thePtnsAry = Regex.Split(thePtns, @"\n");
                    foreach (string a in thePtnsAry)
                    {
                        if (a.Length != 0)
                        {
                            WRITE.WriteLine("\n\t" + a);
                        }
                    }

                    WRITE.WriteLine("\n");


                }

                WRITE.Close();
            }
            catch (Exception e3)
            {
                MessageBox.Show(e3.Message, "Error");
            }
        }

        private void buttonProteinGoAnnotations_Click(object sender, EventArgs e)
        {
            int noProt = resultParser.MyProteins.Count;
            resultParser.PrepareTranslatedDictionary(ref gt);
            int failed = 0;

            foreach (GO.ResultParser.ProteinInfo pi in resultParser.MyProteins)
            {
                Console.WriteLine(pi.ID + "\t" + pi.Description + "\n");
                try
                {
                    //get the go terms associated
                    Console.WriteLine(resultParser.TranslatedResults[pi.ID].Count);
                    List<string> goTerms = gt.AssociationTranslator.Translate(pi.ID);

                    List<Term> myTerms = gt.Terms.FindAll(a => goTerms.Contains(a.id));

                    var grouped = from t in myTerms
                                  group t by t.nameSpace into thegroup
                                  select new { NameSpace = thegroup.Key, TheTerms = thegroup.ToList() };

                    foreach (var g in grouped)
                    {
                        Console.WriteLine(g.NameSpace);

                        foreach (Term theTerm in g.TheTerms)
                        {
                            Console.WriteLine("\t" + theTerm.id + "\t" + theTerm.name + "\t" + theTerm.definition);
                        }
                    }


                }
                catch
                {
                    Console.WriteLine("key not found");
                    failed++;
                }
            }
            Console.WriteLine("Here: " + failed);
            //if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            //{
            //    StreamWriter sr = new StreamWriter(saveFileDialog1.FileName);


            //    sr.Close();
            //}
        }


        //--------------------------------------------------------

    }

}
