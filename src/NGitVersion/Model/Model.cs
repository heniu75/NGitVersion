using System;
using System.Globalization;
using System.Linq;
using LibGit2Sharp;

namespace NGitVersion.Model
{
    public class Model
    {
        private readonly IRepository mRepository;

        private readonly Lazy<string> mRevision;
        private readonly Lazy<string> mShortHash;
        private readonly Lazy<string> mHash;
        private readonly Lazy<string> mBranch;
        private readonly Lazy<string> mHasLocalChange;
        private readonly string mBuildConfig;

        public Model(IRepository repository)
        {
            mRepository = repository;

            mRevision = new Lazy<string>(() => mRepository.Commits.Count().ToString(CultureInfo.InvariantCulture));
            mShortHash = new Lazy<string>(() => mRepository.Commits.First().Sha.Substring(0, 7));
            mHash = new Lazy<string>(() => mRepository.Commits.First().Sha);
            mBranch = new Lazy<string>(() => mRepository.Head.CanonicalName);
            mHasLocalChange = new Lazy<string>(() => mRepository.RetrieveStatus().IsDirty.ToString(CultureInfo.InvariantCulture));
            mBuildConfig = string.Empty;

#if DEBUG
            mBuildConfig = "Debug";
#endif
#if RELEASE
            mBuildConfig = "Release";
#endif
        }

        public string Company        { get { return "Linkusoft"; } }
        public string Product        { get { return "Briga"; } }
        public string Copyright      { get { return "Copyright 2017"; } }
        public string Trademark      { get { return "Briga TM"; } }
        public string Culture        { get { return ""; } }

        public string Major          { get { return "1"; } } // TODO
        public string Minor          { get { return "0"; } } // TODO
        public string Build          { get { return "0"; } } // TODO

        public string Revision       { get { return mRevision.Value; } }

        public string InfoVersion
        {
            get
            {
                var v1 = string.Format("{0}.{1}.{2}.{3}, {4}{5}, {6}", Major, Minor, Build, Revision,
                    Hash, LocalChanges, BuildConfig);
                return v1;
            }
        }

        public string ShortHash      { get { return mShortHash.Value; } }
        public string Hash { get { return mHash.Value; } }
        public string Commit { get { return mHash.Value + LocalChanges; } }
        public string Branch         { get { return mBranch.Value; } }

        public string LocalChanges
        {
            get
            {
                if (Boolean.Parse(mHasLocalChange.Value))
                    return " +changes";
                 return string.Empty;
            }
        }

        public string BuildConfig    { get { return mBuildConfig; } }
    }
}
