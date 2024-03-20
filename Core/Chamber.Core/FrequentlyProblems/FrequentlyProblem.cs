namespace Chamber.Core.FrequentlyProblems;

[Serializable]
public class FrequentlyProblem(long id, string title, string description) : BaseEntity(id)
{
    public readonly string Title = title, Description = description;
}
