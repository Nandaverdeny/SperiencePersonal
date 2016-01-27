(function (window, $) {

  function isAndroid () {
    var ua = (navigator.userAgent).toLowerCase();

    return ua.indexOf('android') > -1;
  }

  function hasTranslate3dSupport () {
    var p = document.createElement('p'),
      has3dSupport,
      transforms = {
        'transform': 'transform',
        'webkitTransform': '-webkit-transform',
        'MozTransform': '-moz-transform',
        'msTransform': '-ms-transform'
      };

    document.body.insertBefore(p, null);

    for (var transform in transforms) {
      if (p.style[transform] !== void 0) {
        p.style[transform] = 'translate3d(1px, 1px, 1px)';
        has3dSupport = window.getComputedStyle(p).getPropertyValue(transforms[transform]);
      }
    }
    document.body.removeChild(p);

    return (has3dSupport !== void 0 && has3dSupport.length && has3dSupport !== 'none');
  }



  var Rightbar = function (target, opts) {
    this.$rightbar = $(target);
    this.$body = $(document.body);
    this.$content = this.$body.find('.jsc-rightbar-content');
    this.rightbarW = this.$rightbar.width();
    this.opts = opts;
    this.meta = this.$rightbar.data('rightbar-options');

    this.init();
  };

  Rightbar.prototype = {

    defaults: {
      trigger: null,
      scrollbarDisplay: true,
      pullCb: function () {},
      pushCb: function () {}
    },

    init: function () {
      this.config = $.extend({}, this.defaults, this.opts, this.meta);

      this.$trigger = this.config.trigger ? this.$body.find(this.config.trigger) : this.$body.find('.jsc-rightbar-trigger');

      this.detect3dEnabled();
      this.attachEvent();
    },

    pushTransitionEndEvent: 'transitionEnd.push webkitTransitionEnd.push transitionend.push msTransitionEnd.push',

    pullTransitionEndEvent: 'transitionEnd.pull webkitTransitionEnd.pull transitionend.pull msTransitionEnd.pull',

    detect3dEnabled: function () {
      if (isAndroid() || !hasTranslate3dSupport()) {
        this.$content.removeClass('jsc-rightbar-pulled');
      }
    },

    attachEvent: function () {
      this.$trigger.on('click', $.proxy(function (e) {
        e.preventDefault();
        this.push();
      }, this));

      this.$content
        .on(this.pushTransitionEndEvent, $.proxy(function () {
          this.detectPushEnd();
          this.config.pushCb();
        }, this))
        .on('click', $.proxy(function () {
          this.pull();
        }, this));
    },

    push: function () {
      if (isAndroid() || !hasTranslate3dSupport()) {
        this.slidePush();
      } else {
        this.$content
          .removeClass('jsc-rightbar-pull-end')
          .addClass('jsc-rightbar-pushed');
      }
    },

    pull: function () {
      if (isAndroid() || !hasTranslate3dSupport()) {
        this.slidePull();
      } else {

        if (!this.$content.hasClass('jsc-rightbar-push-end')) {
          return;
        }

        this.$content.removeClass('jsc-rightbar-pushed');
      }
    },

    slidePull: function () {
      if (this.$content.data('rightbar-first-click') !== 1 || !(this.$content.hasClass('jsc-rightbar-opened'))) {
        return;
      }

      this.$content.stop().animate({
        marginLeft: 0
      }).promise().done($.proxy(function () {
        this.$content.removeClass('jsc-rightbar-opened');
        !this.config.scrollbarDisplay && this.$content.removeClass('jsc-rightbar-scroll-disabled');

        this.config.pullCb();
      }, this));
    },

    slidePush: function () {
      var distance = this.rightbarW + 'px';

      this.$content.stop().animate({
        marginLeft: distance
      }).promise().done($.proxy(function () {
        this.$content.addClass('jsc-rightbar-opened');
        !this.config.scrollbarDisplay && this.$content.removeClass('jsc-rightbar-scroll-disabled');

        if (!this.$content.data('rightbar-first-click')) {
          this.$content.data('rightbar-first-click', 1);
        }
        this.config.pushCb();

      }, this));
    },

    detectPushEnd: function () {
      this.$content.addClass('jsc-rightbar-opened');
      !this.config.scrollbarDisplay && this.$content.removeClass('jsc-rightbar-scroll-disabled');

      this.$content
        .addClass('jsc-rightbar-push-end')
        .off(this.pushTransitionEndEvent)
        .on(this.pullTransitionEndEvent, $.proxy(function () {
          this.detectPullEnd();
          this.config.pullCb();
        }, this));
    },

    detectPullEnd: function () {
      this.$content.removeClass('jsc-rightbar-disabled');
      !this.config.scrollbarDisplay && this.$content.removeClass('jsc-rightbar-scroll-disabled');

      this.$content
        .removeClass('jsc-rightbar-push-end')
        .addClass('jsc-rightbar-pull-end')
        .off(this.pullTransitionEndEvent)
        .on(this.pushTransitionEndEvent, $.proxy(function () {
          this.detectPushEnd();
          this.config.pushCb();
        }, this));
    }

  };

  Rightbar.defaults = Rightbar.prototype.defaults;


  $.fn.rightbar = function (options) {
    return this.each(function () {
      $(this).data('rightbar', new Rightbar(this, options));
    });
  };

})(window, jQuery);
