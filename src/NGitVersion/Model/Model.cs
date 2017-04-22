﻿using System;
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
#if DEBUG
            mBuildConfig = "DEBUG";
#else
            mBuildConfig = "RELEASE";
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
        public string ShortHash      { get { return mShortHash.Value; } }
        public string Hash { get { return mHash.Value; } }
        public string Branch         { get { return mBranch.Value; } }
        public string HasLocalChange { get { return mHasLocalChange.Value; } }
        public string BuildConfig    { get { return mBuildConfig; } }

    }
}
