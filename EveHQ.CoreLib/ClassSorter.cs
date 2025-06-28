using System;
using System.Collections;

namespace EveHQ.CoreLib;

public enum SortDirection
{
    Ascending = 1,
    Descending = 2,
}

public class ClassSorter : IComparer
{
    public ClassSorter()
    {
        SortClasses = new ArrayList();
    }

    public ClassSorter(ArrayList sortClasses)
    {
        SortClasses = sortClasses;
    }

    public ClassSorter(string sortColumn, SortDirection sortDirection)
    {
        SortClasses = new ArrayList { new SortClass(sortColumn, sortDirection) };
    }

    public ArrayList SortClasses { get; }

    public int Compare(object? x, object? y)
    {
        return SortClasses.Count == 0 ? 0 : CheckSort(0, x, y);
    }

    private int CheckSort(int sortLevel, object? myObject1, object? myObject2)
    {
        var returnVal = 0;
        if (SortClasses.Count - 1 < sortLevel) return returnVal;
        
        var valueOf1 = myObject1?.GetType().GetProperty((SortClasses[sortLevel] as SortClass).SortColumn)
            .GetValue(myObject1, null);
            
        var valueOf2 = myObject2?.GetType().GetProperty((SortClasses[sortLevel] as SortClass).SortColumn)
            .GetValue(myObject2, null);
            
        if (valueOf1 != null && valueOf2 != null)
        {
            if ((SortClasses[sortLevel] as SortClass).SortDirection == SortDirection.Ascending)
            {
                returnVal = ((valueOf1 as IComparable)).CompareTo(valueOf2);
            }
            else
            {
                returnVal = ((valueOf2 as IComparable)).CompareTo(valueOf1);
            }
        }

        if (returnVal == 0)
        {
            returnVal = CheckSort(sortLevel + 1, myObject1, myObject2);
        }

        return returnVal;
    }
}

public class SortClass
{
    public SortClass(string sortColumn, SortDirection sortDirection)
    {
        SortColumn = sortColumn;
        SortDirection = sortDirection;
    }

    public string SortColumn { get; set; }

    public SortDirection SortDirection { get; set; }
}