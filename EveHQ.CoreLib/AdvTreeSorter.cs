using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevComponents.AdvTree;

namespace EveHQ.CoreLib;

public class AdvTreeSorter
{
    private static bool sortByTag = false;
    
    public static AdvTreeSortResult Sort(DevComponents.AdvTree.ColumnHeader column, bool sortChildNodes, bool retainLastSortOrder) {
        AdvTree hostedTree = column.AdvTree;
        int columnDisplayIndex = column.DisplayIndex;
        return Sort(hostedTree, columnDisplayIndex, sortChildNodes, retainLastSortOrder);
    }
    
    public static AdvTreeSortResult Sort(AdvTree hostedTree, AdvTreeSortResult sortResult, bool sortChildNodes) {
        return Sort(hostedTree, sortResult.SortedIndex, sortResult.SortedOrder, sortChildNodes, false);
    }
    
    public static AdvTreeSortResult Sort(AdvTree hostedTree, int columnDisplayIndex, bool sortChildNodes, bool retainLastSortOrder)
    {
        return Sort(hostedTree, columnDisplayIndex, AdvTreeSortOrder.Default, sortChildNodes, retainLastSortOrder);
    }

    
    private static AdvTreeSortResult Sort(AdvTree hostedTree, int columnDisplayIndex, AdvTreeSortOrder sortOrder, bool sortChildNodes, bool retainLastSortOrder) {

        //Determine sorting logic based on method parameters
        var lastSortResult = hostedTree.Tag as AdvTreeSortResult;
        int indexToSort = columnDisplayIndex;
        AdvTreeSortOrder orderToSort = sortOrder;
        if (sortOrder == AdvTreeSortOrder.Default)
        {
            orderToSort = AdvTreeSortOrder.Ascending;
        }

        if (lastSortResult != null)
        {
            if (retainLastSortOrder == true)
            {
                indexToSort = lastSortResult.SortedIndex;
                orderToSort = lastSortResult.SortedOrder;
            }
            else
            {
                if (sortOrder == AdvTreeSortOrder.Default)
                {
                    if (indexToSort == lastSortResult.SortedIndex)
                    {
                        orderToSort = (AdvTreeSortOrder)(-(int)lastSortResult.SortedOrder);
                    }
                    else
                    {
                        orderToSort = lastSortResult.SortedOrder;
                    }
                }
                else
                {
                    orderToSort = sortOrder;
                }
            }
        }
        //Set the "true" column index
        int colIdx = indexToSort - 1;

        //Begin UI update
        hostedTree.Cursor = Cursors.WaitCursor;
        hostedTree.BeginUpdate();
        
        //Check if we are to look at the tag instead of text
        if (hostedTree.Columns[colIdx].EditorType == eCellEditorType.Custom)
        {
            sortByTag = true;
        }
        else
        {
            sortByTag = false;
        }

        //Reset the old image
        if (lastSortResult != null)
        {
            hostedTree.Columns[lastSortResult.SortedIndex - 1].SortDirection = eSortDirection.None;
        }
        
        //Set new image
        hostedTree.Columns[colIdx].ImageAlignment = eColumnImageAlignment.Right;
        if (orderToSort == AdvTreeSortOrder.Ascending)
        {
            hostedTree.Columns[colIdx].SortDirection = eSortDirection.Ascending;
        }
        else
        {
            hostedTree.Columns[colIdx].SortDirection = eSortDirection.Descending;
        }

        hostedTree.Nodes.Sort(new AdvTreeSortComparer(colIdx, orderToSort, sortByTag));
        if (sortChildNodes == true)
        {
            PerformIterativeSort(hostedTree.Nodes, colIdx, orderToSort);
        }

        //End the update
        hostedTree.EndUpdate();
        hostedTree.Cursor = Cursors.Default;

        //Return and store the result
        var newSortResult = new AdvTreeSortResult(indexToSort, orderToSort);
        hostedTree.Tag = newSortResult;
        return newSortResult;
    }

    private static void PerformIterativeSort(NodeCollection sortNodeCollection, int colIdx, AdvTreeSortOrder order)
    {
        foreach (Node sortNode in sortNodeCollection)
        {
            if (sortNode.Nodes.Count > 0)
            {
                //' Do a sort on these
                sortNode.Nodes.Sort(new AdvTreeSortComparer(colIdx, order, sortByTag));
                //' Check for additional nodes to sort
                PerformIterativeSort(sortNode.Nodes, colIdx, order);
            }
        }
    }
}

public class AdvTreeSortComparer : IComparer
{
    private int _col;
    private AdvTreeSortOrder _order;
    private bool _sortTag = false;
    private double _doubleX, _doubleY;
    private DateTime _dateX, _dateY;

    public AdvTreeSortComparer()
    {
        _col = 0;
        _order = AdvTreeSortOrder.Ascending;
        _sortTag = false;
    }

    public AdvTreeSortComparer(int column,  AdvTreeSortOrder order, bool sortTag)
    {
        _col = column;
        _order = order;
        _sortTag = sortTag;
    }
    
    public int Compare(object x, object y)
    {
        int returnVal;
        double tempDbl = 0;
        DateTime _dateX, _dateY;
        NumberStyles NumberStyle = NumberStyles.Number | NumberStyles.AllowParentheses;

        if (_sortTag == false)
        {
            if (Double.TryParse((x as Node).Cells[_col].Text, NumberStyle, null, out tempDbl) &&
                Double.TryParse((y as Node).Cells[_col].Text, NumberStyle, null, out tempDbl)) {
                //Parse the two objects passed as a parameter as a Double
                _doubleX = Double.Parse(((x as Node).Cells[_col].Text), NumberStyle);
                _doubleY = Double.Parse(((y as Node).Cells[_col].Text), NumberStyle);
                //Compare the two numbers
                returnVal = _doubleX.CompareTo(_doubleY);
            }
            else
            {
                if (DateTime.TryParse((x as Node).Cells[_col].Text, out _dateX) && 
                    DateTime.TryParse((y as Node).Cells[_col].Text, out _dateY)) {
                    //Parse the two objects passed as a parameter as a Date
                    //Compare the two numbers
                    returnVal = _dateX.CompareTo(_dateY);
                } else {
                    returnVal = System.String.Compare((x as Node).Cells[_col].Text, (y as Node).Cells[_col].Text, StringComparison.Ordinal);
                }
            }
        }
        else
        {
            if (Double.TryParse((x as Node).Cells[_col].Tag.ToString(), NumberStyle, null, out tempDbl) &&
                Double.TryParse((y as Node).Cells[_col].Tag.ToString(), NumberStyle, null, out tempDbl)) {
                //Parse the two objects passed as a parameter as a Double
                _doubleX = Double.Parse(((x as Node).Cells[_col].Tag.ToString()), NumberStyle);
                _doubleY = Double.Parse(((y as Node).Cells[_col].Tag.ToString()), NumberStyle);
                //Compare the two numbers
                returnVal = _doubleX.CompareTo(_doubleY);
            }
            else
            {
                if (DateTime.TryParse((x as Node).Cells[_col].Tag.ToString(), out _dateX) && 
                    DateTime.TryParse((y as Node).Cells[_col].Tag.ToString(), out _dateY)) {
                    //Parse the two objects passed as a parameter as a Date
                    //Compare the two numbers
                    returnVal = _dateX.CompareTo(_dateY);
                } else {
                    returnVal = System.String.Compare((x as Node).Cells[_col].Tag.ToString(), (y as Node).Cells[_col].Tag.ToString(), StringComparison.Ordinal);
                }
            }            
        }
        
        
        //Determine whether the sort order is descending.
        if (_order == AdvTreeSortOrder.Descending)
        {
            //Invert the value returned by String.Compare.
            returnVal *= -1;
        }

        return returnVal;
    }
}

public class AdvTreeSortResult
{
    public int SortedIndex;
    public AdvTreeSortOrder SortedOrder;

    public AdvTreeSortResult()
    {
        SortedIndex = 1;
        SortedOrder = AdvTreeSortOrder.Default;
    }

    public AdvTreeSortResult(int columnIndex, AdvTreeSortOrder sortOrder)
    {
        SortedIndex = columnIndex;
        SortedOrder = sortOrder;
    }
}

[DefaultValue(Default)]
public enum AdvTreeSortOrder
{
    Ascending = -1,
    Default = 0,
    Descending = 1,
}