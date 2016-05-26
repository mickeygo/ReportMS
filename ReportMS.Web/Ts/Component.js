var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var RmsComponent;
(function (RmsComponent) {
    var MyCommentBox = (function (_super) {
        __extends(MyCommentBox, _super);
        function MyCommentBox() {
            _super.apply(this, arguments);
        }
        MyCommentBox.prototype.render = function () {
            this.setState({ s1: "commentBox2" });
            var s = this.state.s1;
            return (React.createElement("div", {className: "commentBox"}, "Hello, world!I am a CommentBox." + ' ' + "state: ", s));
        };
        return MyCommentBox;
    }(React.Component));
    RmsComponent.MyCommentBox = MyCommentBox;
    RmsComponent.CommentBox = React.createClass({
        getInitialState: function () {
            return { s1: 1 };
        },
        componentDidMount: function () {
        },
        componentDidUpdate: function (p, s) {
            this.setState({ s1: 2 });
        },
        render: function () {
            return (React.createElement("div", {className: "commentBox"}, "Hello, world!I am a CommentBox." + ' ' + "state: ", this.state.s1));
        }
    });
    var ListGroup = (function (_super) {
        __extends(ListGroup, _super);
        function ListGroup() {
            _super.apply(this, arguments);
        }
        ListGroup.prototype.render = function () {
            return (React.createElement("div", {className: "list-group"}, this.state.map(function (val, index) {
                return (React.createElement("a", {className: "list-group-item"}, val, React.createElement("span", {class: "badge"}, React.createElement("span", {class: "glyphicon glyphicon-trash"}))));
            })));
        };
        return ListGroup;
    }(React.Component));
    RmsComponent.ListGroup = ListGroup;
})(RmsComponent || (RmsComponent = {}));
ReactDOM.render(React.createElement(RmsComponent.MyCommentBox, null), document.getElementById('container'));
ReactDOM.render(React.createElement(RmsComponent.CommentBox, null), document.getElementById('helloworld'));
