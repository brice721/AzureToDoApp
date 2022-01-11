using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text;

namespace Web.Pages
{
    public partial class ToDo : ComponentBase
    {
        public ToDo() { }

        [Parameter]
        public string NewTodo { get; set; }

        [Parameter]
        public string EditRowStyle { get; set; } = "none";

        [Parameter]
        public string TextColor { get; set; }

        private const string ServiceEndpoint = "http://localhost:7071/api";
        private const string All = "All";
        private const string Active = "Active";
        private const string Completed = "Completed";

        private IList<ToDoItem> _toDos = new List<ToDoItem>();
        private IList<ToDoItem> _originalToDos = new List<ToDoItem>();
        private ToDoItem _editItem = new();

        private void ToggleView(string viewName)
        {
            switch (viewName)
            {
                case "All":
                    _toDos = _originalToDos;
                    _toDos = _toDos.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();
                    break;
                case "Active":
                    _toDos = _originalToDos;
                    _toDos = _toDos.Where(x => !x.IsComplete).ToList();
                    break;
                case "Completed":
                    _toDos = _originalToDos;
                    _toDos = _toDos.Where(x => x.IsComplete).ToList();
                    break;
            }
        }

        private void EditItem(string id)
        {
            if (_toDos is not null)
            {
                _editItem = _toDos.Single(i => i.Id.ToString() == id.ToString());
                EditRowStyle = "table-row";
            }
        }

        private async void AddToDo()
        {
            if (!string.IsNullOrWhiteSpace(NewTodo))
            {
                var obj = JObject.FromObject(new { Title = NewTodo });
                await Http.PostAsync(@$"{ServiceEndpoint}/Create", new StringContent(obj.ToString(), Encoding.UTF8, "application/json"))
                    .ContinueWith(async (response) => {                    
                        await GetToDos();                    
                        NewTodo = string.Empty;                    
                        StateHasChanged();                
                    });
            }

            NewTodo = string.Empty;
            await GetToDos();
        }

        private async Task MarkItemComplete(ToDoItem item, object toDoItem)
        {
            if ((bool)toDoItem)
            {
                var obj = JObject.FromObject(new { Id = item.Id, Title = item.Title, IsComplete = true });

                await Http.PutAsync(@$"{ServiceEndpoint}/Update", new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));

                await GetToDos();
            }
            else
            {
                var obj = JObject.FromObject(new { Id = item.Id, Title = item.Title, IsComplete = false });

                await Http.PutAsync(@$"{ServiceEndpoint}/Update", new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));

                await GetToDos();
            }
        }

        private async Task SaveItem(string id)
        {
            if (_editItem is not null)
            {
                var obj = JObject.FromObject(new { Id = _editItem.Id, Title = _editItem.Title });
                await Http.PutAsync(@$"{ServiceEndpoint}/Update", new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
            }

            await GetToDos();
            EditRowStyle = "none";
        }

        private async Task DeleteItem(string id)
        {
            await Http.DeleteAsync($"{ServiceEndpoint}/Delete/{id}");
            await GetToDos();
            EditRowStyle = "none";
        }

        private async Task GetToDos()
        {
            _toDos = await Http.GetFromJsonAsync<ToDoItem[]>(@$"{ServiceEndpoint}/Get");
            _originalToDos = _toDos;
        }

        protected override async Task OnInitializedAsync()
        {
            _toDos = await Http.GetFromJsonAsync<ToDoItem[]>(@$"{ServiceEndpoint}/Get");
            _originalToDos = _toDos;
        }
    }
}
