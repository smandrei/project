using System.Collections.Generic;
using System.Linq;

namespace WFAEntity.API
{
    static class DatabaseRequest
    {
        public struct NewStudent
        {
            public int NId { get; set; }
            public string NName { get; set; }
            public string NSurname { get; set; }
            public int NIdGroup { get; set; }
            public string NGroup { get; set; }
            public NewStudent(int nId, string nName, string nSurname, int nIdGroup, string nGroup)
            {
                this.NId = nId;
                this.NName = nName;
                this.NSurname = nSurname;
                this.NIdGroup = nIdGroup;
                this.NGroup = nGroup;
            }
        }
        static DatabaseRequest()
        {
        }
        public static bool IsUser(MyDBContext objectMyDBContext, string login, string password)
        {
            var tmp = (
                from tmpUser in objectMyDBContext.Users.ToList<User>()
                where tmpUser.Login.CompareTo(login) == 0 && tmpUser.Password.CompareTo(password) == 0
                select tmpUser
                      ).ToList();
            if (tmp.Count == 1)
                return true;
            return false;
        }
        public static IEnumerable<NewStudent> GetStudentsWithGroups(MyDBContext objectMyDBContext)
        {
            return (
                from tmpStudent in objectMyDBContext.Students.ToList<Student>()
                from tmpGroup in objectMyDBContext.Groups.ToList<Group>()
                where tmpStudent.Id == tmpGroup.Id
                select (
                new NewStudent(tmpStudent.Id, tmpStudent.Name, tmpStudent.Surname, tmpGroup.Id, tmpGroup.Name)
                )
                       ).ToList();
        }
        public static IEnumerable<Group> GetGroups(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Groups.ToList();
        }
    }
}

