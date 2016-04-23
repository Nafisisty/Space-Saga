using GART.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Here_History
{
    internal class ShortHistory : ARItem
    {
        public int HistoryId { get; set; }
        private string description;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged(() => Description);
                }
            }

        }
    }
}
