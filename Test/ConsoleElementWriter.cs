using System.Text;

public class ConsoleElementWriter : IWriter<Element>
{
    private StringBuilder builder;
    private int index;

    public ConsoleElementWriter()
    {
        builder = new();
        index = 1;
    }

    public void Append(Element user)
    {
        OnAppend();
        builder
            .AppendLine()
            .Append(index++)
            .Append(". Name: ")
            .Append(user.Name);
    }

    public void Write()
    {
        OnWrite();
        Console.WriteLine(builder);
    }

    protected virtual void OnAppend()
    {
        if (builder.Length < 1)
        {
            builder.Append("Some elements of the periodic table: ");
        }
    }

    protected virtual void OnWrite()
    {
        if (builder.Length < 1)
        {
            builder.Append("Elements not appended.");
        }
    }
}
