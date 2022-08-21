using System;

namespace BlogApp.Common.Model.Security
{
    public sealed class Role
    {
        public static Role PUBLIC = new Role("PUBLIC");
        public static Role WRITER = new Role("WRITER");
        public static Role EDITOR = new Role("EDITOR");
        
        private readonly string _roleName;

        private Role(string roleName)
        {
            _roleName = roleName;
        }

        public override string ToString()
        {
            return _roleName;
        }

        public static Role Parse(string roleName)
        {
            return roleName switch
            {
                "PUBLIC" => PUBLIC,
                "WRITER" => WRITER,
                "EDITOR" => EDITOR,
                _ => throw new ArgumentException("Invalid role")
            };
        }
    }
}
