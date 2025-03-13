using Moq;
using ToDoApp.Persistence.Entities;

namespace ToDoApp.Test;

public class GetTodoItemsHandlerTests
{
    private readonly Mock<ITodoRepository> _repositoryMock;
    private readonly GetTodoItemsHandler _handler;

    public GetTodoItemsHandlerTests()
    {
        _repositoryMock = new Mock<ITodoRepository>();
        _handler = new GetTodoItemsHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_List_Of_TodoItems()
    {
        // Arrange
        var todoItems = new List<TodoItem>
        {
            new TodoItem { Id = Guid.NewGuid().to, Title = "Task 1", IsCompleted = false },
            new TodoItem { Id = Guid.NewGuid().ToString(), Title = "Task 2", IsCompleted = true }
        };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(todoItems);

        // Act
        var result = await _handler.Handle(new GetTodoItemsQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(2);
    }
}