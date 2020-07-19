(function (global) {
    'use strict';
    var lang = ['i'], //this will cause an exception
      intlProps = {
          'writable': true,
          'enumerable': false,
          'configurable': true
      };
    //check if supported
    if ('Intl' in global) {
        //It's supported, don't need to do anything!
        console.log('i18n appears to be supported.');
        return;
    }
    //Check if Chrome's implementation is available
    if ('v8Intl' in global) {
        //create a reference to Intl
        intlProps.value = global.v8Intl;
        Object.defineProperty(global, "Intl", intlProps);
        //patch V8 functionality
        patchV8();
        return;
    }
    //no match found...
    return console.warn('bummer, i18n API is not supported \u2639');
    /**
     * patchV8
     * monkey-patch the standard functions in V8 to use i18n formatters
     **/
    function patchV8() {
        var date = new Date();
        var num = 12345;
        var str = "";
        var objsToPatch = [];
        var timeDefaults = Object.create(null);
        timeDefaults.hour = timeDefaults.minute = timeDefaults.second = 'numeric';
        objsToPatch.push({
            test: date,
            func: 'toLocaleString',
            proto: Date.prototype,
            formatter: global.Intl.DateTimeFormat,
            defaults: undefined
        });
        objsToPatch.push({
            test: date,
            func: 'toLocaleDateString',
            proto: Date.prototype,
            formatter: global.Intl.DateTimeFormat,
            defaults: undefined
        });
        objsToPatch.push({
            test: date,
            func: 'toLocaleTimeString',
            proto: Date.prototype,
            formatter: global.Intl.DateTimeFormat,
            defaults: timeDefaults
        });
        objsToPatch.push({
            test: num,
            func: 'toLocaleString',
            proto: Number.prototype,
            formatter: global.Intl.NumberFormat,
            defaults: undefined
        });
        //start patching as needed
        objsToPatch.forEach(checkAndPatch);
        //patch String.prototype.localeCompare
        patchLocaleCompare();

        function patchLocaleCompare() {
            try {
                str.localeCompare("", lang);
            } catch (e) {
                console.log("localeCompare already supported. No need to patch.");
                return;
            }
            String.prototype.localeCompare = (function (old) {
                //create the patched function, and then return it
                var patchedFunction = function (that, locales, options) {
                    var collator;
                    //Call the original "native code" function
                    if (locales === undefined && options === undefined) {
                        if (that === undefined) {
                            return old.call(this);
                        }
                        return old.call(this, that);
                    }
                    //clean up and normalize values
                    locales = String(locales).split(',');
                    options = Object(options);

                    //collate this and that
                    collator = new global.Intl.Collator(locales, options);
                    return collator.compare(this, that);
                };
                //monkey patch!
                return patchedFunction;
            }(String.prototype.localeCompare));

            try {
                str.localeCompare("", lang);
            } catch (e) {
                console.log("String.prototype.localeCompare patched succesfully.");
                return;
            }
            console.warn("Was not able to succesfully patch localeCompare");
        }
        //Check if needs to be pached and patch if it does
        function checkAndPatch(obj) {
            var patched = false,
              supported = true;
            try {
                obj.test[obj.func](lang);
                supported = false;
                patch(obj.func, obj.proto, obj.formatter, obj.defaults);
                patched = true;
                obj.test[obj.func](lang);
                supported = false;
            } catch (e) {
                supported = true;
            }
            if (!patched && supported) {
                console.log(obj.func + "already supported. No need to patch.");
                return;
            }
            if (patched && supported) {
                console.log("Succefully patched " + obj.func);
                return;
            }
            console.warn("Was not able to succesfully patch " + obj.func);
        }
        //monkey patch by keeping native functionality when needed
        function patch(functionName, ofPrototype, i18nformatter, i18nDefaults) {
            //override native function of a given prototype
            //(e.g., Number.prototype['toLocaleString'])
            ofPrototype[functionName] = (function (old, formatter, defaults) {
                //create the patched function, and then return it
                var patchedFunction = function (locales, options) {
                    var optionsClone = Object.create({});
                    //Call the original "native code" function
                    if (locales === undefined && options === undefined) {
                        return old.call(this);
                    }

                    //clean up and normalize values
                    locales = String(locales).split(',');
                    options = Object(options);

                    //clone options, so not to change the original object
                    for (var i in options) {
                        optionsClone[i] = options[i];
                    }

                    //set defaults for output, as Chrome does not do this sometimes
                    if (defaults) {
                        for (var i in defaults) {
                            if (!optionsClone.hasOwnProperty(i)) {
                                optionsClone[i] = defaults[i];
                            }
                        }
                    }
                    //localize and format the object and return result
                    return (new formatter(locales, optionsClone)).format(this);
                };
                //monkey patch!
                return patchedFunction;
            }(ofPrototype[functionName], i18nformatter, i18nDefaults));
        }
    }
}(this));