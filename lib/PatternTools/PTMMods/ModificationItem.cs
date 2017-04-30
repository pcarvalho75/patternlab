using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternTools.PTMMods
{
    /// <summary>
    /// Used by the parameter class
    /// </summary>
    [Serializable]
    public class ModificationItem
    {
        public string AminoAcid { get; set; }
        public string Representation
        {
            get
            {
                return AminoAcid + "(" + DeltaMass + ")";
            }
        }

        public string Description { get; set; }

        public double DeltaMass { get; set; }

        public bool OnlyCTerminus { get; set; }
        public bool OnlyNTerminus { get; set; }
        public bool IsVariable { get; set; }
        public bool IsActive { get; set; }

        
        /// <summary>
        /// This constructor should not be used and is just here to make this serializable
        /// </summary>
        public ModificationItem() { }

        public ModificationItem(
            string aminoacid,
            string description,
            double deltaMass,
            bool onlyCTerminus = false,
            bool onlyNTerminus = false,
            bool isVariable = false,
            bool isActive = false
            )
        {
            this.AminoAcid = aminoacid;
            this.DeltaMass = deltaMass;
            this.Description = description;
            this.OnlyCTerminus = onlyCTerminus;
            this.OnlyNTerminus = onlyNTerminus;
            this.IsVariable = isVariable;
            IsActive = isActive;
        }
    }

}
