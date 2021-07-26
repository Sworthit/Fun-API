using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunApi.Constants
{
    public static class Messages
    {
        public static string GetSuccess = "Name succesfully retrieved from the database";
        public static string GetFailed = "Could not retrieve name from the database";
        public static string GetNamesSuccess = "Names succesfully retireved from the database";
        public static string GetNamesFailed = "Could not retrieves names from the databse";
        public static string PostSuccess = "Name succesfully saved in the database";
        public static string PostFailedUserExist = "Name already exist in the database";
        public static string GeneratedNameSuccess = "Name succesfully saved in the database. Enjoy your unique name";
        public static string NameDoesNotExist = "Could not find name in the database. Add it first to check if it's a valid name";
        public static string GenerateName = "Name generated.";
        public static string NameIsNotValid = "Name should only contain letters";
        public static string NameIsValid = "Name is valid";
        public static string NameExistsInNameDatabase = "Name is not unique, it exists in name database.";
        public static string ElonMuskException = "Name must be valid, not unlike Elon's child";
        public static string GetNamesAmount(int amount)
        {
            return "Avarage name length calculated, names found : " + amount.ToString();
        }
        public static string GeneratedNameFailed(DateTime generatedDate)
        {
            return "That name is already taken. Generated at : " + generatedDate.ToString();
        }

    }
}
