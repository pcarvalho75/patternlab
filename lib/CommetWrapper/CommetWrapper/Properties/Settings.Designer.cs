﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CometWrapper.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("none")]
        public string ModificationLib {
            get {
                return ((string)(this["ModificationLib"]));
            }
            set {
                this["ModificationLib"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("none")]
        public string SearchSettings {
            get {
                return ((string)(this["SearchSettings"]));
            }
            set {
                this["SearchSettings"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("# comet_version 2016.01 rev. 2\r\n# Comet MS/MS search engine parameters file.\r\n# E" +
            "verything following the \'#\' symbol is treated as a comment.\r\n\r\ndatabase_name = [" +
            "DBPATH]\r\ndecoy_search = 0                       # 0=no (default), 1=concatenated" +
            " search, 2=separate search\r\n\r\nnum_threads = 0                        # 0=poll CP" +
            "U to set num threads; else specify num threads directly (max 64)\r\n\r\n#\r\n# masses\r" +
            "\n#\r\npeptide_mass_tolerance = [PEPTIDEMASSTOLERANCE]\r\npeptide_mass_units = 2     " +
            "            # 0=amu, 1=mmu, 2=ppm\r\nmass_type_parent = 1                   # 0=av" +
            "erage masses, 1=monoisotopic masses\r\nmass_type_fragment = 1                 # 0=" +
            "average masses, 1=monoisotopic masses\r\nprecursor_tolerance_type = 0           # " +
            "0=MH+ (default), 1=precursor m/z; only valid for amu/mmu tolerances\r\nisotope_err" +
            "or = 1                      # 0=off, 1=on -1/0/1/2/3 (standard C13 error), 2= -8" +
            "/-4/0/4/8 (for +4/+8 labeling)\r\n\r\n#\r\n# search enzyme\r\n#\r\nsearch_enzyme_number = " +
            "[ENZYME]               # choose from list at end of this params file\r\nnum_enzyme" +
            "_termini = [DIGESTIONSPECIFICITY]                 # valid values are 1 (semi-dig" +
            "ested), 2 (fully digested, default), 8 N-term, 9 C-term\r\nallowed_missed_cleavage" +
            " = [MISSEDCLEAVAGES]            # maximum value is 5; for enzyme search\r\n\r\n#\r\n# " +
            "Up to 9 variable modifications are supported\r\n# format:  <mass> <residues> <0=va" +
            "riable/1=binary> <max_mods_per_peptide> <term_distance> <n/c-term> <required>\r\n#" +
            "     e.g. 79.966331 STY 0 3 -1 0 0\r\n#\r\nvariable_mod01 = [VARMOD1]\r\nvariable_mod0" +
            "2 = [VARMOD2]\r\nvariable_mod03 = [VARMOD3]\r\nvariable_mod04 = [VARMOD4]\r\nvariable_" +
            "mod05 = [VARMOD5]\r\nvariable_mod06 = [VARMOD6]\r\nvariable_mod07 = 0.0 X 0 3 -1 0 0" +
            "\r\nvariable_mod08 = 0.0 X 0 3 -1 0 0\r\nvariable_mod09 = 0.0 X 0 3 -1 0 0\r\nmax_vari" +
            "able_mods_in_peptide = [MAXVARIABLEMODSPERPEPTIDE]\r\nrequire_variable_mod = 0\r\n\r\n" +
            "#\r\n# fragment ions\r\n#\r\n# ion trap ms/ms:  1.0005 tolerance, 0.4 offset (mono mas" +
            "ses), theoretical_fragment_ions = 1\r\n# high res ms/ms:    0.02 tolerance, 0.0 of" +
            "fset (mono masses), theoretical_fragment_ions = 0\r\n#\r\nfragment_bin_tol = [FRAGME" +
            "NTBINTOLERANCE]              # binning to use on fragment ions\r\nfragment_bin_off" +
            "set = [FRAGMENTBINOFFSET]              # offset position to start the binning (0" +
            ".0 to 1.0)\r\ntheoretical_fragment_ions = [THEORETICALFRAGMENTIONS]          # 0=u" +
            "se flanking peaks, 1=M peak only\r\nuse_A_ions = [A_ION]\r\nuse_B_ions = [B_ION]\r\nus" +
            "e_C_ions = [C_ION]\r\nuse_X_ions = [X_ION]\r\nuse_Y_ions = [Y_ION]\r\nuse_Z_ions = [Z_" +
            "ION]\r\nuse_NL_ions = [NL_ION]                        # 0=no, 1=yes to consider NH" +
            "3/H2O neutral loss peaks\r\n\r\n#\r\n# output\r\n#\r\noutput_sqtstream = 0                " +
            "   # 0=no, 1=yes  write sqt to standard output\r\noutput_sqtfile = 1              " +
            "       # 0=no, 1=yes  write sqt file\r\noutput_txtfile = 0                     # 0" +
            "=no, 1=yes  write tab-delimited txt file\r\noutput_pepxmlfile = 0                 " +
            " # 0=no, 1=yes  write pep.xml file\r\noutput_percolatorfile = 0              # 0=n" +
            "o, 1=yes  write Percolator tab-delimited input file\r\noutput_outfiles = 0        " +
            "            # 0=no, 1=yes  write .out files\r\nprint_expect_score = 1             " +
            "    # 0=no, 1=yes to replace Sp with expect in out & sqt\r\nnum_output_lines = 4  " +
            "                 # num peptide results to show\r\nshow_fragment_ions = 0          " +
            "       # 0=no, 1=yes for out files only\r\n\r\nsample_enzyme_number = [SAMPLENZYME] " +
            "              # Sample enzyme which is possibly different than the one applied t" +
            "o the search.\r\n                                       # Used to calculate NTT & " +
            "NMC in pepXML output (default=1 for trypsin).\r\n\r\n#\r\n# mzXML parameters\r\n#\r\nscan_" +
            "range = 0 0                       # start and scan scan range to search; 0 as 1s" +
            "t entry ignores parameter\r\nprecursor_charge = 0 0                 # precursor ch" +
            "arge range to analyze; does not override any existing charge; 0 as 1st entry ign" +
            "ores parameter\r\noverride_charge = 0                    # 0=no, 1=override precur" +
            "sor charge states, 2=ignore precursor charges outside precursor_charge range, 3=" +
            "see online\r\nms_level = 2                           # MS level to analyze, valid " +
            "are levels 2 (default) or 3\r\nactivation_method = ALL                # activation" +
            " method; used if activation method set; allowed ALL, CID, ECD, ETD, PQD, HCD, IR" +
            "MPD\r\n\r\n#\r\n# misc parameters\r\n#\r\ndigest_mass_range = [MASSRANGEMIN] [MASSRANGEMAX" +
            "]       # MH+ peptide mass range to analyze\r\nnum_results = 100                  " +
            "    # number of search hits to store internally\r\nskip_researching = 1           " +
            "        # for \'.out\' file output only, 0=search everything again (default), 1=do" +
            "n\'t search if .out exists\r\nmax_fragment_charge = 3                # set maximum " +
            "fragment charge state to analyze (allowed max 5)\r\nmax_precursor_charge = 6      " +
            "         # set maximum precursor charge state to analyze (allowed max 9)\r\nnucleo" +
            "tide_reading_frame = 0           # 0=proteinDB, 1-6, 7=forward three, 8=reverse " +
            "three, 9=all six\r\nclip_nterm_methionine = 0              # 0=leave sequences as-" +
            "is; 1=also consider sequence w/o N-term methionine\r\nspectrum_batch_size = 0     " +
            "           # max. # of spectra to search at a time; 0 to search the entire scan " +
            "range in one loop\r\ndecoy_prefix = Reverse__               # decoy entries are de" +
            "noted by this string which is pre-pended to each protein accession\r\noutput_suffi" +
            "x =                        # add a suffix to output base names i.e. suffix \"-C\" " +
            "generates base-C.pep.xml from base.mzXML input\r\nmass_offsets =                  " +
            "       # one or more mass offsets to search (values substracted from deconvolute" +
            "d precursor mass)\r\n\r\n#\r\n# spectral processing\r\n#\r\nminimum_peaks = 10            " +
            "         # required minimum number of peaks in spectrum to search (default 10)\r\n" +
            "minimum_intensity = 0                  # minimum intensity value to read in\r\nrem" +
            "ove_precursor_peak = 0              # 0=no, 1=yes, 2=all charge reduced precurso" +
            "r peaks (for ETD)\r\nremove_precursor_tolerance = 1.5       # +- Da tolerance for " +
            "precursor removal\r\nclear_mz_range = [CLEARMZRANGEMIN] [CLEARMZRANGEMAX]         " +
            "      # for iTRAQ/TMT type data; will clear out all peaks in the specified m/z r" +
            "ange\r\n\r\n#\r\n# additional modifications\r\n#\r\n\r\nadd_Cterm_peptide = [FIXEDCTERMINUS]" +
            "\r\nadd_Nterm_peptide = [FIXEDNTERMINUS]\r\nadd_Cterm_protein = 0.0\r\nadd_Nterm_prote" +
            "in = 0.0\r\n\r\nadd_G_glycine = [ADD_G]                 # added to G - avg.  57.0513" +
            ", mono.  57.02146\r\nadd_A_alanine = [ADD_A]                 # added to A - avg.  " +
            "71.0779, mono.  71.03711\r\nadd_S_serine = [ADD_S]                  # added to S -" +
            " avg.  87.0773, mono.  87.03203\r\nadd_P_proline = [ADD_P]                 # added" +
            " to P - avg.  97.1152, mono.  97.05276\r\nadd_V_valine = [ADD_V]                  " +
            "# added to V - avg.  99.1311, mono.  99.06841\r\nadd_T_threonine = [ADD_T]        " +
            "       # added to T - avg. 101.1038, mono. 101.04768\r\nadd_C_cysteine = [ADD_C]  " +
            "           # added to C - avg. 103.1429, mono. 103.00918\r\nadd_L_leucine = [ADD_L" +
            "]                 # added to L - avg. 113.1576, mono. 113.08406\r\nadd_I_isoleucin" +
            "e = [ADD_I]              # added to I - avg. 113.1576, mono. 113.08406\r\nadd_N_as" +
            "paragine = [ADD_N]              # added to N - avg. 114.1026, mono. 114.04293\r\na" +
            "dd_D_aspartic_acid = [ADD_D]           # added to D - avg. 115.0874, mono. 115.0" +
            "2694\r\nadd_Q_glutamine = [ADD_Q]               # added to Q - avg. 128.1292, mono" +
            ". 128.05858\r\nadd_K_lysine = [ADD_K]                  # added to K - avg. 128.172" +
            "3, mono. 128.09496\r\nadd_E_glutamic_acid = [ADD_E]           # added to E - avg. " +
            "129.1140, mono. 129.04259\r\nadd_M_methionine = [ADD_M]              # added to M " +
            "- avg. 131.1961, mono. 131.04048\r\nadd_O_ornithine = [ADD_O]               # adde" +
            "d to O - avg. 132.1610, mono  132.08988\r\nadd_H_histidine = [ADD_H]              " +
            " # added to H - avg. 137.1393, mono. 137.05891\r\nadd_F_phenylalanine = [ADD_F]   " +
            "        # added to F - avg. 147.1739, mono. 147.06841\r\nadd_R_arginine = [ADD_R] " +
            "               # added to R - avg. 156.1857, mono. 156.10111\r\nadd_Y_tyrosine = [" +
            "ADD_Y]                # added to Y - avg. 163.0633, mono. 163.06333\r\nadd_W_trypt" +
            "ophan = [ADD_W]              # added to W - avg. 186.0793, mono. 186.07931\r\nadd_" +
            "B_user_amino_acid = [ADD_B]         # added to B - avg.   0.0000, mono.   0.0000" +
            "0\r\nadd_J_user_amino_acid = [ADD_J]         # added to J - avg.   0.0000, mono.  " +
            " 0.00000\r\nadd_U_user_amino_acid = [ADD_U]         # added to U - avg.   0.0000, " +
            "mono.   0.00000\r\nadd_X_user_amino_acid = [ADD_X]         # added to X - avg.   0" +
            ".0000, mono.   0.00000\r\nadd_Z_user_amino_acid = [ADD_Z]         # added to Z - a" +
            "vg.   0.0000, mono.   0.00000\r\n\r\n#\r\n# COMET_ENZYME_INFO _must_ be at the end of " +
            "this parameters file\r\n#\r\n[COMET_ENZYME_INFO]\r\n0.  No_enzyme              0      " +
            "-           -\r\n1.  Trypsin                1      KR          P\r\n2.  Trypsin/P   " +
            "           1      KR          -\r\n3.  Lys_C                  1      K           P" +
            "\r\n4.  Lys_N                  0      K           -\r\n5.  Arg_C                  1 " +
            "     R           P\r\n6.  Asp_N                  0      D           -\r\n7.  CNBr   " +
            "                1      M           -\r\n8.  Glu_C                  1      DE      " +
            "    P\r\n9.  PepsinA                1      FL          P\r\n10. Chymotrypsin        " +
            "   1      FWYL        P\r\n")]
        public string CometParamsTemplate {
            get {
                return ((string)(this["CometParamsTemplate"]));
            }
            set {
                this["CometParamsTemplate"] = value;
            }
        }
    }
}
