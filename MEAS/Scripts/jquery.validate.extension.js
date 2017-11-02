/*!
 *  jquery validation extension
 */

$.extend($.validator.messages, {
            required: "必选字段",
            remote: "请修正该字段",
            email: "请输入正确格式的电子邮件",
            url: "请输入合法的网址",
            date: "请输入合法的日期",
            dateISO: "请输入合法的日期 (ISO).",
            number: "请输入合法的数字",
            digits: "只能输入整数",
            creditcard: "请输入合法的信用卡号",
            equalTo: "请再次输入相同的值",
            accept: "请输入拥有合法后缀名的字符串",
            maxlength: $.validator.format("请输入一个长度最多是 {0} 的字符串"),
            minlength: $.validator.format("请输入一个长度最少是 {0} 的字符串"),
            rangelength: $.validator.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
            range: $.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
            max: $.validator.format("请输入一个最大为 {0} 的值"),
            min: $.validator.format("请输入一个最小为 {0} 的值")
 });

//添加字符串验证-只能包括中文字、英文字母、数字和下划线
 function appendSepecialCharRule(){
     $.validator.addMethod("specialChar", function (value, element) {
         return this.optional(element) || /^[u0391-uFFE5w]+$/.test(value);
     }, "只能包括英文字母、数字和下划线");
 }

 function setValidatorDefaults() {
     $.validator.setDefaults({
         debug: true, //调试模式取消submit的默认提交功能

         // errorClass: "label.error", //默认为错误的样式类为：error

         focusInvalid: true, //当为false时，验证无效时，没有焦点响应

         //键盘事件触发，默认是不触发的，而是仅在失去焦点时触发
         onkeyup: function (element, event) {
             if (event.which === 9 && this.elementValue(element) === "") {
                 return;
             } else {
                 this.element(element);
             }
         },

         //验证全部通过后触发
         submitHandler: function (form) {   //表单提交句柄,为一回调函数，带一个参数：form
             executeLogin();
         },

         //当前项验证失败后触发
         highlight: function (element) {
             $(element).parent("div.form-group").removeClass("has-success").addClass("has-error");
             $(element).siblings("span").removeClass("glyphicon-ok").addClass("glyphicon-remove");
         },

         //当前项验证成功后触发
         success: function (element) {
             $(element).parent("div.form-group").removeClass("has-error").addClass("has-success");
             $(element).siblings("span").removeClass("glyphicon-remove").addClass("glyphicon-ok");
         }
     });
 }


