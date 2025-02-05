namespace BlogApi.ViewModels
{
    // classe de padronização de erros
    public class ResultViewModel<T>
    {

        public ResultViewModel(T data, List<string> error)
        {
            Data = data;
            Errors = error;
        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(List<string> error)
        {
            Errors = error;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }

        public T Data { get; private set; }

        public List<string> Errors { get; private set; } = new();

    }
}
