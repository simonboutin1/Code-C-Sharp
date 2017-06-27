using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Administration
{
    class AccessGroup
    {
        private Int32 _intAccessGrpID;
        public Int32 AccessGroupID { get { return _intAccessGrpID; } set { _intAccessGrpID = value; } }
        private string _sAccessGrpDesc;
        public string AccessGroupDesc { get { return _sAccessGrpDesc; } set { _sAccessGrpDesc = value; } }
        public AccessGroup(Int32 pintAccessGrpID, string psAccessGrpDesc)
        {
            _intAccessGrpID = pintAccessGrpID;
            _sAccessGrpDesc = psAccessGrpDesc;
        }
        public override string ToString()
        {
            return _intAccessGrpID.ToString() + " - " + _sAccessGrpDesc;
        }
    }
}
