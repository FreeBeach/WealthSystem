using System.Collections.Generic;

namespace UsefulTool
{
    /// <summary>
    /// 涉及子叶节点相关方法操作
    /// </summary>
    public class TreeEventLeaf
    {
        private List<TreeEvent> leafs;
        public virtual void AddLeaf(TreeEvent tree)
        {
            if (tree == null) return;
            if (leafs == null) leafs = new List<TreeEvent>();
            if (!leafs.Contains(tree)) leafs.Add(tree);
        }
        public virtual void RemoveLeaf(TreeEvent tree)
        {
            if (leafs == null || tree==null ) return;
            if (leafs.Contains(tree))
                leafs.Remove(tree);
        }
    }
}