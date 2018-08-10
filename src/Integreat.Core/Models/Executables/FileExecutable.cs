using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Integreat.Plugins;

namespace Integreat.Core
{
    public abstract class FileExecutable : ExecutableBase
    {
        //protected FileExecutable(IProcess process, IFileStorage fileStorage, ExecutableReference reference)
        //    : this(process, fileStorage, reference.Timeout, reference.Parameters, reference.File)
        //{

        //}

        protected FileExecutable(IFileStorage fileStorage, string file)
        {
            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException(nameof(file), $"A filename value for File is required to execute {GetType().FullName} executable.");

            FileStorage = fileStorage;
            File = file.Trim();
        }

        protected string File { get; private set; }
        protected IFileStorage FileStorage { get; private set; }

        public override string Name => $"Type: {GetType().Name}, File: {File}";

        protected override void OnExecute(ExecutableContext context)
        { 
            var file = FileStorage.GetFile(Path.Combine(context.ExecutablesDirectory, File));
            if (!file.Exists)
                throw new FileNotFoundException($"Executable file '{file.FullPath}' not found.");

            OnExecute(context, file);
        }

        protected abstract void OnExecute(ExecutableContext context, IFile file);
    }
}
