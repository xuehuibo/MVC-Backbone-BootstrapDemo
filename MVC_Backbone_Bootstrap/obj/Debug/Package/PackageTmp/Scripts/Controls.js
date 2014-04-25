//本文件主要是用Backbone.View 封装了 Bootstrap的组件,用于统一组件样式
//注册事件需要Extend

Tools.Namespace.register("Controls");

//Form
Controls.Form = Backbone.View.extend({
    tagName: "form",
    className: "form-container",
    attributes:{
        role:"form"
    },
    initialize: function () {
        this.render();
    },
    render: function () {
        this.$el.addClass(this.className);
        this.$el.attr(this.attributes);
    }
});

//Button
Controls.Button = Backbone.View.extend({
    tagName: "a",
    className: "btn btn-default",
    initialize: function () {
        this.render();
    },
    render: function () {
        this.$el.addClass(this.className);
    }
});

//Textbox
Controls.Textbox = Backbone.View.extend({
    tagName: "input",
    attributes: {
        type: "text"
    },
    className:"form-control",
    initialize: function () {
        this.render();
    },
    render: function () {
        this.$el.addClass(this.className);
        this.$el.attr(this.attributes);
    }
});

