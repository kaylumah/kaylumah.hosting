using System.Collections.Generic;

namespace Kaylumah.Ssg.Manager.Site.Service
{
    public class FileMetaData : Dictionary<string, object>
    {
        public string Layout
        {
            get
            {
                if (ContainsKey(nameof(Layout).ToLower()))
                {
                    return (string)this[nameof(Layout).ToLower()];
                }
                return null;
            }
            set
            {
                this[nameof(Layout).ToLower()] = value;
            }
        }
    }
}