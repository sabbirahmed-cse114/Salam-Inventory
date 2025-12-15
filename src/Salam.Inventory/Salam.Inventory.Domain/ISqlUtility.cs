namespace Salam.Inventory.Domain
{
    public interface ISqlUtility
    {
        Task<TReturn> ExecuteScalar_Async<TReturn>(string storedProcedure_Name, IDictionary<string, object> parameters = null);
        IDictionary<string, object> Execute_StoredProcedure(string storedProcedure_Name, IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null);
        Task<IDictionary<string, object>> ExecuteStoredProcedure_Async(string storedProcedure_Name, IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null);
        Task<(IList<TReturn> result, IDictionary<string, object> outValues)> QueryWithStoredProcedure_Async<TReturn>(string storedProcedure_Name, IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null) where TReturn : class, new();
    }
}