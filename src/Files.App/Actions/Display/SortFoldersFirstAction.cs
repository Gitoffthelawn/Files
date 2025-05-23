﻿// Copyright (c) Files Community
// Licensed under the MIT License.

namespace Files.App.Actions
{
	internal sealed partial class SortFoldersFirstAction : ObservableObject, IToggleAction
	{
		private readonly IDisplayPageContext context;

		public string Label
			=> Strings.SortFoldersFirst.GetLocalizedResource();

		public string Description
			=> Strings.SortFoldersFirstDescription.GetLocalizedResource();

		public bool IsOn
			=> !context.SortFilesFirst && !context.SortDirectoriesAlongsideFiles;

		public SortFoldersFirstAction()
		{
			context = Ioc.Default.GetRequiredService<IDisplayPageContext>();

			context.PropertyChanged += Context_PropertyChanged;
		}

		public Task ExecuteAsync(object? parameter = null)
		{
			context.SortFilesFirst = false;
			context.SortDirectoriesAlongsideFiles = false;
			LayoutHelpers.UpdateOpenTabsPreferences();

			return Task.CompletedTask;
		}

		private void Context_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName is nameof(IDisplayPageContext.SortFilesFirst) or nameof(IDisplayPageContext.SortDirectoriesAlongsideFiles))
				OnPropertyChanged(nameof(IsOn));
		}
	}
}
