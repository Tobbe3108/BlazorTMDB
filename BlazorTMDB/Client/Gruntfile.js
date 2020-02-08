/// <binding ProjectOpened='watch-css' />
module.exports = function (grunt) {
    grunt.initConfig({// get the configuration info from package.json
        pkg: grunt.file.readJSON('package.json'),// create a clean task to remove production resource files under wwwroot
        clean: ["wwwroot/css/*"],// PostCSS - Tailwindcss and Autoprefixer
        postcss: {
            options: {
                map: true, // inline sourcemaps
                processors: [
                    require('tailwindcss')(),
                    require('autoprefixer') // add vendor prefixes
                ]
            },
            dist: {
                expand: true,
                cwd: 'src/css/',
                src: ['*.css'],
                dest: 'wwwroot/css/',
                ext: '.css'
            }
        },
        watch: {
            postcss: {
                files: 'src/css/**/*.css',
                tasks: ['compile-css'],
                options: {
                    interrupt: true
                }
            }
        }
    });// Load Grunt Plugins
    grunt.registerTask('compile-css', ['clean', 'postcss']);
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.registerTask('watch-css', ['watch:postcss']);
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-postcss");
};