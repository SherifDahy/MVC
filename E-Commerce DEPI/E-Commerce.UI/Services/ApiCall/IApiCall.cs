namespace ApiConsume;
public interface IApiCall
{
    Task<IEnumerable<T>> GetAllAsync<T>(string url);
    Task<IEnumerable<T>> GetAllByIdAsync<T>(string url,int id);
    Task<T> GetByIdAsync<T>(string url, int id);
    Task<bool> CreateAsync<T>(string url, T entity);
    

    public Task<T2> PostReturnAsync<T1, T2>(string url, T1 entity);
    Task<bool> UpdateAsync<T>(string url, int id, T entity);
    Task<bool> DeleteAsync<T>(string url, int id);
}
