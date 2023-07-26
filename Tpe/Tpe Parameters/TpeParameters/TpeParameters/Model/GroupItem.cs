using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TpeParameters.Helpers;

namespace TpeParameters.Model
{
    public class GroupItem
    {
        public GroupItem(int id, string name, GroupTypes groupType, string description,
            List<ParameterItem> parameters)
        {
            _id = id;
            _name = name;
            _groupType = groupType;
            _description = description;
            _parameters = parameters;
        }

        private int _id;
        private string _name;
        private GroupTypes _groupType;
        private string _description;

        private List<ParameterItem> _parameters;

        public int Id
        {
            get { return _id; }
        }
        public string Name
        {
            get { return _name; }
        }

        public GroupTypes GroupType
        {
            get { return _groupType; }
        }

        public string Description
        {
            get { return _description; }
        }

        public List<ParameterItem> Parameters
        {
            get { return _parameters; }
        }

    }
}
