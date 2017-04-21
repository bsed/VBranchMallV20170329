(function () {
    var f = null;
    var h = "google_conversion_id google_conversion_format google_conversion_type google_conversion_order_id google_conversion_language google_conversion_value google_conversion_domain google_conversion_label google_conversion_color google_disable_viewthrough google_remarketing_only google_remarketing_for_search google_conversion_items google_custom_params google_conversion_date google_conversion_time google_conversion_js_version onload_callback opt_image_generator google_is_call google_conversion_page_url".split(" ");
    function i(b) {
        return b != f ? escape(b.toString()) : ""
    }
    function j(b, c) {
        var g = i(c);
        if ("" != g) {
            var a = i(b);
            if ("" != a) return "&".concat(a, "=", g)
        }
        return ""
    }
    function k(b) {
        var c = typeof b;
        return b == f || "object" == c || "function" == c ? f : String(b).replace(/,/g, "\\,").replace(/;/g, "\\;").replace(/=/g, "\\=")
    }
    function l(b) {
        var c;
        b = b.google_custom_params;
        if (!b || "object" != typeof b || "function" == typeof b.join) c = "";
        else {
            var g = [];
            for (c in b) if (Object.prototype.hasOwnProperty.call(b, c)) {
                var a = b[c];
                if (a && "function" == typeof a.join) {
                    for (var d = [], e = 0; e < a.length; ++e) {
                        var q = k(a[e]);
                        q != f && d.push(q)
                    }
                    a = 0 == d.length ? f : d.join(",")
                } else a = k(a); (d = k(c)) && a != f && g.push(d + "=" + a)
            }
            c = g.join(";")
        }
        return "" == c ? "" : "&".concat("data=", encodeURIComponent(c))
    }
    function m(b) {
        return "number" != typeof b && "string" != typeof b ? "" : i(b.toString())
    }
    function n(b) {
        if (!b) return "";
        b = b.google_conversion_items;
        if (!b) return "";
        for (var c = [], g = 0, a = b.length; g < a; g++) {
            var d = b[g],
            e = [];
            d && (e.push(m(d.value)), e.push(m(d.quantity)), e.push(m(d.item_id)), e.push(m(d.adwords_grouping)), e.push(m(d.sku)), c.push("(" + e.join("*") + ")"))
        }
        return 0 < c.length ? "&item=" + c.join("") : ""
    }
    function o(b, c, g) {
        var a = [];
        if (b) {
            var d = b.screen;
            d && (a.push(j("u_h", d.height)), a.push(j("u_w", d.width)), a.push(j("u_ah", d.availHeight)), a.push(j("u_aw", d.availWidth)), a.push(j("u_cd", d.colorDepth)));
            b.history && a.push(j("u_his", b.history.length))
        }
        g && "function" == typeof g.getTimezoneOffset && a.push(j("u_tz", -g.getTimezoneOffset()));
        c && ("function" == typeof c.javaEnabled && a.push(j("u_java", c.javaEnabled())), c.plugins && a.push(j("u_nplug", c.plugins.length)), c.mimeTypes && a.push(j("u_nmime", c.mimeTypes.length)));
        return a.join("")
    }
    function p(b, c, g) {
        var a = "";
        if (c) {
            var a = a + j("ref", c.referrer != f ? c.referrer.toString().substring(0, 256) : ""),
            d,
            c = 2;
            try {
                if (b.top.document == b.document) c = 0;
                else {
                    var e = b.top;
                    try {
                        d = !!e.location.href || "" === e.location.href
                    } catch (q) {
                        d = !1
                    }
                    d && (c = 1)
                }
            } catch (J) { }
            d = c;
            e = "";
            e = g ? g : 1 == d ? b.top.location.href : b.location.href;
            a += j("url", e != f ? e.toString().substring(0, 256) : "");
            a += j("frm", d)
        }
        return a
    }
    function r(b) {
        return b && b.location && b.location.protocol && "https:" == b.location.protocol.toString().toLowerCase() ? "https:" : "http:"
    }
    function s(b, c, g) {
        return r(b) + "//" + (g.google_remarketing_only ? "googleads.g.doubleclick.net" : g.google_conversion_domain || "www.googleadservices.com") + "/pagead/" + c
    }
    function t() {
        var b = u,
        c = navigator,
        g = document,
        a = u,
        d = "/?";
        "landing" == a.google_conversion_type && (d = "/extclk?");
        var d = s(b, [a.google_remarketing_only ? "viewthroughconversion/" : "conversion/", i(a.google_conversion_id), d, "random=", i(a.google_conversion_time)].join(""), a),
        e;
        a: 
        {
            e = a.google_conversion_language;
            if (e != f) {
                e = e.toString();
                if (2 == e.length) {
                    e = j("hl", e);
                    break a
                }
                if (5 == e.length) {
                    e = j("hl", e.substring(0, 2)) + j("gl", e.substring(3, 5));
                    break a
                }
            }
            e = ""
        }
        c = [j("cv", a.google_conversion_js_version), j("fst", a.google_conversion_first_time), j("num", a.google_conversion_snippets), j("fmt", a.google_conversion_format), j("value", a.google_conversion_value), j("label", a.google_conversion_label), j("oid", a.google_conversion_order_id), j("bg", a.google_conversion_color), e, j("guid", "ON"), j("disvt", a.google_disable_viewthrough), j("is_call", a.google_is_call), n(a), o(b, c, a.google_conversion_date), p(b, g, a.google_conversion_page_url), l(a), a.google_remarketing_for_search && !a.google_conversion_domain ? "&srr=n" : ""].join("");
        c = d + c;
        g = function (a, b, c) {
            return '<img height="' + c + '" width="' + b + '" border="0" src="' + a + '" />'
        };
        return 0 == a.google_conversion_format && a.google_conversion_domain == f ? '<a href="' + (r(b) + "//services.google.com/sitestats/" + ({
            ar: 1,
            bg: 1,
            cs: 1,
            da: 1,
            de: 1,
            el: 1,
            en_AU: 1,
            en_US: 1,
            en_GB: 1,
            es: 1,
            et: 1,
            fi: 1,
            fr: 1,
            hi: 1,
            hr: 1,
            hu: 1,
            id: 1,
            is: 1,
            it: 1,
            iw: 1,
            ja: 1,
            ko: 1,
            lt: 1,
            nl: 1,
            no: 1,
            pl: 1,
            pt_BR: 1,
            pt_PT: 1,
            ro: 1,
            ru: 1,
            sk: 1,
            sl: 1,
            sr: 1,
            sv: 1,
            th: 1,
            tl: 1,
            tr: 1,
            vi: 1,
            zh_CN: 1,
            zh_TW: 1
        }[a.google_conversion_language] ? a.google_conversion_language + ".html" : "en_US.html") + "?cid=" + i(a.google_conversion_id)) + '" target="_blank">' + g(c, 135, 27) + "</a>" : 1 < a.google_conversion_snippets || 3 == a.google_conversion_format ? g(c, 1, 1) : '<iframe name="google_conversion_frame" width="' + (2 == a.google_conversion_format ? 200 : 300) + '" height="' + (2 == a.google_conversion_format ? 26 : 13) + '" src="' + c + '" frameborder="0" marginwidth="0" marginheight="0" vspace="0" hspace="0" allowtransparency="true" scrolling="no">' + g(c.replace(/\?random=/, "?frame=0&random="), 1, 1) + "</iframe>"
    }
    function v() {
        return new Image
    };
    var u = window;
    if (u) if (/[\?&;]google_debug/.exec(document.URL) != f) {
        var w = u,
        x = document.getElementsByTagName("head")[0];
        x || (x = document.createElement("head"), document.getElementsByTagName("html")[0].insertBefore(x, document.getElementsByTagName("body")[0]));
        var y = document.createElement("script");
        y.src = s(window, "conversion_debug_overlay.js", w);
        x.appendChild(y)
    } else {
        try {
            var z;
            var A = u;
            "landing" == A.google_conversion_type || !A.google_conversion_id || A.google_remarketing_only && A.google_disable_viewthrough ? z = !1 : (A.google_conversion_date = new Date, A.google_conversion_time = A.google_conversion_date.getTime(), A.google_conversion_snippets = "number" == typeof A.google_conversion_snippets && 0 < A.google_conversion_snippets ? A.google_conversion_snippets + 1 : 1, "number" != typeof A.google_conversion_first_time && (A.google_conversion_first_time = A.google_conversion_time), A.google_conversion_js_version = "7", 0 != A.google_conversion_format && (1 != A.google_conversion_format && 2 != A.google_conversion_format && 3 != A.google_conversion_format) && (A.google_conversion_format = 1), z = !0);
            if (z && (document.write(t()), u.google_remarketing_for_search && !u.google_conversion_domain)) {
                var B = u,
                C, D = u,
                E;
                E = r(D) + "//www.google.com/ads/user-lists/" + [i(B.google_conversion_id), "/?random=", Math.floor(1E9 * Math.random())].join("");
                C = E += [j("label", B.google_conversion_label), j("fmt", "3"), p(D, document, B.google_conversion_page_url)].join("");
                var F = v;
                "function" === typeof B.opt_image_generator && (F = B.opt_image_generator);
                var G = F();
                C += j("async", "1");
                G.src = C;
                G.onload = function () { }
            }
        } catch (H) { }
        for (var I = u,
        K = 0; K < h.length; K++) I[h[K]] = f
    };
})();