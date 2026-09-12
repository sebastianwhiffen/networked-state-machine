let SessionLoad = 1
let s:so_save = &g:so | let s:siso_save = &g:siso | setg so=0 siso=0 | setl so=-1 siso=-1
let v:this_session=expand("<sfile>:p")
doautoall SessionLoadPre
silent only
silent tabonly
cd ~/source/networked-state-machine
if expand('%') == '' && !&modified && line('$') <= 1 && getline(1) == ''
  let s:wipebuf = bufnr('%')
endif
let s:shortmess_save = &shortmess
set shortmess+=aoO
badd +46 src/client/client.cs
badd +24 ~/source/MagicOnion/samples/ChatApp/ChatApp.Console/Program.cs
badd +4 ~/source/nvim/init.lua
badd +1 ~/source/networked-state-machine
badd +12 ~/source/MagicOnion/samples/ChatApp/ChatApp.Shared/Hubs/IChatHub.cs
badd +1 ~/source/MagicOnion/samples/ChatApp/ChatApp.Shared/Hubs/IChatHubReceiver.cs
badd +15 ~/source/MagicOnion/samples/ChatApp/ChatApp.Server/ChatHub.cs
badd +1 todo
badd +36 test/state_machine_test.cs
badd +44 term://~/source/MagicOnion//260563:/bin/bash
badd +1 ~/source/Godot-Advanced-State-Machine-First-Person-Controller
badd +1 ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine
badd +1 ~/source/nvim/lua/dap_conf.lua
badd +53 ~/source/MagicOnion/src/MagicOnion.Client/DynamicClient/DynamicStreamingHubClientBuilder.cs
badd +14 ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/MagicOnionClientSourceGenerator.Emitter.cs
badd +31 term://~/source/MagicOnion//31342:/bin/zsh
badd +6 ~/source/MagicOnion/src/MagicOnion.Client/DynamicClient/DynamicStreamingHubClientFactoryProvider.cs
badd +82 ~/source/MagicOnion/tests/MagicOnion.Client.SourceGenerator.Tests/GenerateGenericsTest.cs
badd +29 ~/source/MagicOnion/tests/MagicOnion.Client.SourceGenerator.Tests/RunTest.cs
badd +8 ~/source/MagicOnion/src/MagicOnion.Client/MagicOnionClientGenerationAttribute.cs
badd +32 ~/source/MagicOnion/src/MagicOnion.Client/StreamingHubClientFactoryProvider.cs
badd +1 term://~/source/MagicOnion//31438:/bin/zsh
badd +688 term://~/source/MagicOnion//31465:/bin/zsh
badd +6 term://~/source/nvim//31519:/bin/zsh
badd +11 ~/source/MagicOnion/samples/ChatApp/ChatApp.Unity/Assets/Scripts/InitialSettings.cs
badd +1 ~/source/SourceGenerator/Program.cs
badd +1 ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine/run_state_script.gd
badd +1 test/NetworkedStateMachine.Client.Generator.Test/GeneratorTest.cs
badd +15 ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/MagicOnionClientSourceGenerator.cs
badd +3 ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/CodeAnalysis/MagicOnionServiceCollection.cs
badd +1 ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/CodeAnalysis/ReferenceSymbols.cs
badd +30 ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/CodeAnalysis/SerializationInfoCollector.cs
badd +25 ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/GenerationContext.cs
badd +12 ~/source/nvim/lua/lsp_configs/init.lua
badd +19 /private/var/folders/k7/r0z35szj6dbd535w5bgsbpmr0000gn/T/MetadataAsSource/9d2febc6e8814207bfb11999f8dc1e01/DecompilationMetadataAsSourceFileProvider/61948eba5ca346f78c9dd11a117ed8d8/INamedTypeSymbol.cs
badd +55 /private/var/folders/k7/r0z35szj6dbd535w5bgsbpmr0000gn/T/MetadataAsSource/9d2febc6e8814207bfb11999f8dc1e01/DecompilationMetadataAsSourceFileProvider/2a764c65a7d4498db17533f146d56076/IncrementalGeneratorInitializationContext.cs
argglobal
%argdel
$argadd ~/source/networked-state-machine
set stal=2
tabnew +setlocal\ bufhidden=wipe
tabnew +setlocal\ bufhidden=wipe
tabrewind
edit test/NetworkedStateMachine.Client.Generator.Test/GeneratorTest.cs
let s:save_splitbelow = &splitbelow
let s:save_splitright = &splitright
set splitbelow splitright
wincmd _ | wincmd |
vsplit
1wincmd h
wincmd w
let &splitbelow = s:save_splitbelow
let &splitright = s:save_splitright
wincmd t
let s:save_winminheight = &winminheight
let s:save_winminwidth = &winminwidth
set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1
exe 'vert 1resize ' . ((&columns * 94 + 94) / 188)
exe 'vert 2resize ' . ((&columns * 93 + 94) / 188)
argglobal
balt test/state_machine_test.cs
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 31 - ((30 * winheight(0) + 26) / 52)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 31
normal! 0
lcd ~/source/networked-state-machine
wincmd w
argglobal
if bufexists(fnamemodify("~/source/networked-state-machine/todo", ":p")) | buffer ~/source/networked-state-machine/todo | else | edit ~/source/networked-state-machine/todo | endif
if &buftype ==# 'terminal'
  silent file ~/source/networked-state-machine/todo
endif
balt ~/source/networked-state-machine/test/state_machine_test.cs
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 13 - ((12 * winheight(0) + 26) / 52)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 13
normal! 0
lcd ~/source/networked-state-machine
wincmd w
exe 'vert 1resize ' . ((&columns * 94 + 94) / 188)
exe 'vert 2resize ' . ((&columns * 93 + 94) / 188)
tabnext
edit ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/MagicOnionClientSourceGenerator.cs
let s:save_splitbelow = &splitbelow
let s:save_splitright = &splitright
set splitbelow splitright
wincmd _ | wincmd |
vsplit
1wincmd h
wincmd w
let &splitbelow = s:save_splitbelow
let &splitright = s:save_splitright
wincmd t
let s:save_winminheight = &winminheight
let s:save_winminwidth = &winminwidth
set winminheight=0
set winheight=1
set winminwidth=0
set winwidth=1
exe 'vert 1resize ' . ((&columns * 94 + 94) / 188)
exe 'vert 2resize ' . ((&columns * 93 + 94) / 188)
tcd ~/source/MagicOnion
argglobal
balt ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/CodeAnalysis/ReferenceSymbols.cs
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 13 - ((12 * winheight(0) + 26) / 52)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 13
normal! 0
wincmd w
argglobal
if bufexists(fnamemodify("~/source/MagicOnion/tests/MagicOnion.Client.SourceGenerator.Tests/RunTest.cs", ":p")) | buffer ~/source/MagicOnion/tests/MagicOnion.Client.SourceGenerator.Tests/RunTest.cs | else | edit ~/source/MagicOnion/tests/MagicOnion.Client.SourceGenerator.Tests/RunTest.cs | endif
if &buftype ==# 'terminal'
  silent file ~/source/MagicOnion/tests/MagicOnion.Client.SourceGenerator.Tests/RunTest.cs
endif
balt ~/source/MagicOnion/src/MagicOnion.Client.SourceGenerator/MagicOnionClientSourceGenerator.cs
setlocal foldmethod=manual
setlocal foldexpr=0
setlocal foldmarker={{{,}}}
setlocal foldignore=#
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 40 - ((36 * winheight(0) + 26) / 52)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 40
normal! 087|
wincmd w
exe 'vert 1resize ' . ((&columns * 94 + 94) / 188)
exe 'vert 2resize ' . ((&columns * 93 + 94) / 188)
tabnext
edit ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine/run_state_script.gd
tcd ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine
argglobal
setlocal foldmethod=manual
setlocal foldexpr=<SNR>67_GDScriptFoldLevel()
setlocal foldmarker={{{,}}}
setlocal foldignore=
setlocal foldlevel=0
setlocal foldminlines=1
setlocal foldnestmax=20
setlocal foldenable
silent! normal! zE
let &fdl = &fdl
let s:l = 1 - ((0 * winheight(0) + 26) / 52)
if s:l < 1 | let s:l = 1 | endif
keepjumps exe s:l
normal! zt
keepjumps 1
normal! 0
lcd ~/source/Godot-Advanced-State-Machine-First-Person-Controller/addons/JehenoAdvancedFirstPersonController/PlayerCharacter/StateMachine
tabnext 1
set stal=1
if exists('s:wipebuf') && len(win_findbuf(s:wipebuf)) == 0 && getbufvar(s:wipebuf, '&buftype') isnot# 'terminal'
  silent exe 'bwipe ' . s:wipebuf
endif
unlet! s:wipebuf
set winheight=1 winwidth=20
let &shortmess = s:shortmess_save
let &winminheight = s:save_winminheight
let &winminwidth = s:save_winminwidth
let s:sx = expand("<sfile>:p:r")."x.vim"
if filereadable(s:sx)
  exe "source " . fnameescape(s:sx)
endif
let &g:so = s:so_save | let &g:siso = s:siso_save
set hlsearch
doautoall SessionLoadPost
unlet SessionLoad
" vim: set ft=vim :
