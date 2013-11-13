using LibGit2Sharp.Core;
using LibGit2Sharp.Core.Compat;

namespace LibGit2Sharp
{
    public class HistoryDivergence
    {
        private readonly Lazy<Commit> commonAncestor;

        /// <summary>
        /// Needed for mocking purposes.
        /// </summary>
        protected HistoryDivergence()
        { }

        internal HistoryDivergence(Repository repo, Commit since, Commit until)
        {
            Since = since;
            Until = until;
            
            commonAncestor = new Lazy<Commit>(() => repo.Commits.FindCommonAncestor(since, until));
            Tuple<int?, int?> div = Proxy.git_graph_ahead_behind(repo.Handle, until, since);
            AheadBy = div.Item1;
            BehindBy = div.Item2;
        }

        public virtual Commit Since { get; private set; }

        public virtual Commit Until { get; private set; }

        public virtual int? AheadBy { get; private set; }

        public virtual int? BehindBy { get; private set; }

        public virtual Commit CommonAncestor
        {
            get
            {
                return commonAncestor.Value;
            }
        }

    }
}
