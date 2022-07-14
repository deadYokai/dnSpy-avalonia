/*
    Copyright (C) 2014-2019 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
// using System.Windows;
// using System.Windows.Input;
using dnSpy.Contracts.Controls;
using dnSpy.Text.Editor;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using AvaloniaEdit.Rendering;

namespace dnSpy.MainApp {
	sealed partial class MainWindow : Window {
		public MainWindow(object? content){
			InitializeIfNeeded();
			//InitializeComponent();
			//TODO
			//contentPresenter.Content = content;
			//CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, (s, e) => Close(), (s, e) => e.CanExecute = true));
		}

		private protected  bool HandleHoriztonalScroll(IInputElement element, short delta) {
			if (element is TextView wpfTextView) {
				if (wpfTextView.Options.WordWrapIndentation == 0) {
					var deltaDouble = (double)delta;
					var currentViewport = wpfTextView.Document.LineCount;

					bool isReverseScroll = deltaDouble < 0;

					if (isReverseScroll) {
						if (currentViewport > 0) {
							if (currentViewport + deltaDouble < 0) {
								deltaDouble = 0 - currentViewport;
							}

							
							//wpfTextView.ViewScroller.ScrollViewportHorizontallyByPixels(deltaDouble);
							return true;
						}
					}
					else {
						var maxScroll = Math.Max(currentViewport, wpfTextView.MaxWidth - wpfTextView.Width + WpfTextViewConstants.EXTRA_HORIZONTAL_SCROLLBAR_WIDTH);
						if (currentViewport < maxScroll) {
							if (currentViewport + deltaDouble > maxScroll) {
								deltaDouble = maxScroll - currentViewport;
							}

							//wpfTextView.ViewScroller.ScrollViewportHorizontallyByPixels(deltaDouble);
							return true;
						}
					}
				}

				return false;
			}
			return HandleHoriztonalScroll(element, delta);
		}
	}
}
