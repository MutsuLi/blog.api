using System;
namespace Blog.Model.Models
{

    #region snippet
    /// <summary>
    /// TodoItem demo
    /// </summary>
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
    #endregion
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
