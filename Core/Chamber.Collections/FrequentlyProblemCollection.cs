using Chamber.Core.FrequentlyProblems;

namespace Chamber.Collections;

internal class FrequentlyProblemCollection : UniqeCollection<FrequentlyProblem>
{
    public override bool Contains(FrequentlyProblem item)
    {
        return Contains(i => i.Title == item.Title);
    }

    public override void Delete(FrequentlyProblem item)
    {
        Delete(i => i.Title == item.Title);
    }
}
