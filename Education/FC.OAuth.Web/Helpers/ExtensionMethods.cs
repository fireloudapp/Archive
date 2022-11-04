using System.Diagnostics;
using FC.Admin.Services.Entities;
using Newtonsoft.Json;

namespace FC.Admin.Services.Helpers;

public static class ExtensionMethods
{
    public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
    {
        if (users == null) return null;

        return users.Select(x => x.WithoutPassword());
    }

    public static User WithoutPassword(this User user)
    {
        if (user == null) return null;

        user.PasswordHash = null;
        return user;
    }

    /// <summary>
    /// Converts object into JSON Data
    /// </summary>
    /// <param name="obj">model object</param>
    /// <returns>JSON Data</returns>
    public static string DumpToJSON(this object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }

    /// <summary>
    /// Get Class Name and method for easy loging
    /// </summary>
    /// <returns>A string value with class name and method name</returns>
    public static string LogClassAndMethod()
    {
        var stackTrace = new StackTrace(true);
        string logMsg = string.Empty;
        foreach (var r in stackTrace.GetFrames())
        {
            logMsg = $"Filename: {r.GetFileName()} Method: {r.GetMethod()}"; break;
            //Line: r.GetFileLineNumber(),
            //Column: r.GetFileColumnNumber());
        }
        return logMsg;
    }
}