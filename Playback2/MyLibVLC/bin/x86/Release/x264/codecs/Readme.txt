These are binary codecs for use with MPlayer. They are useless for normal
Windows players (like Windows Media Player, QuickTime, RealPlayer, ...) as
they only contain the DLLs without installer and other fancy stuff needed
to use them with common Windows players.

Put the files contained in this archive in a directory where MPlayer will find
them. The default directory is /usr/local/lib/codecs/ ($prefix/lib/codecs/) if
you are compiling from source, but you can change that value by passing the
'--with-codecsdir' option to './configure'.

If you use a prebuilt MPlayer package it will most likely be /usr/lib/codecs,
see the documentation of your package for details.

In the past /usr/local/lib/win32 or /usr/lib/win32 was the default directory,
some packages as well as a few other Unix players like xine and avifile still
use it, refer to their documentation for further details.

On Windows if you are using a prebuilt MPlayer, put the contents of this
package in the codecs folder within your main MPlayer installation folder.
