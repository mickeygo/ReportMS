/**
 * @summary     RmsComponent
 * @description TypeScript React
 * @version     1.0.0
 * @file        Component.js
 * @author      gang.yang
 * @requires    jquery.js, bootstrap
 * @copyright   Advantech.com
 */

namespace RmsComponent {

    // Test Demo
    export class MyCommentBox extends React.Component<any, { s1: string }> {
        //constructor(props: any) {
        //    super(props);
        //    this.setState({ s1: "commentBox2" });
        //}
        render() {
            this.setState({ s1: "commentBox2" });
            var s = this.state.s1;
            return (
                <div className="commentBox">
                    Hello, world!I am a CommentBox.
                    state: {s}
                </div>
            );
        }
    }

    // Test React Demo
    export var CommentBox = React.createClass({
        getInitialState: function () {
            return { s1: 1 };
        },

        componentDidMount: function () {
        },

        componentDidUpdate: function (p, s) {
            this.setState({ s1: 2 });
        },

        render: function () {
            return (
                <div className="commentBox">
                    Hello, world!I am a CommentBox.
                    state: {this.state.s1}
                </div>
            );
        }
    });

    // list-group component bootstrap
    interface IListGroup {
        id: string;
        className: string;
    }

    export class ListGroup extends React.Component<IListGroup, string[]> {
        render() {
            return (
                <div className="list-group">
                    {
                        this.state.map(function (val, index) {
                            return (<a className="list-group-item">{val}
                                <span class="badge"><span class="glyphicon glyphicon-trash"></span></span>
                            </a>);
                        })
                    }
                </div>
            )
        }
    }
}

// React Call 
ReactDOM.render(
    <RmsComponent.MyCommentBox />,
    document.getElementById('container')
);

ReactDOM.render(
    <RmsComponent.CommentBox />,
    document.getElementById('helloworld')
);