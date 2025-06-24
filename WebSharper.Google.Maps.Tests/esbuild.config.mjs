import { existsSync, cpSync, readdirSync } from 'fs'
import { build } from 'esbuild'

if (existsSync('./build/Content/WebSharper/')) {
    cpSync('./build/Content/WebSharper/', './bin/netstandard2.0/html/Content/WebSharper/', { recursive: true });
}

const files = readdirSync('./build/Scripts/WebSharper/WebSharper.Google.Maps.Tests/');

files.forEach(file => {
    if (file.endsWith('.js')) {
        var options =
        {
            entryPoints: ['./build/Scripts/WebSharper/WebSharper.Google.Maps.Tests/' + file],
            bundle: true,
            minify: true,
            format: 'iife',
            outfile: 'bin/netstandard2.0/html/Scripts/WebSharper/' + file,
            globalName: 'wsbundle'
        };

        console.log("Bundling:", file);
        build(options);
    }
});