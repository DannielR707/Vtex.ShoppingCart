var React = require('react');

var DevList = React.createClass({

  handleAddToCart: function() {
    //Todo
  },

  render: function() {
    return(
      <tr className="product">
        <td>
          <img src={this.props.singleItem.Avatar} alt="Avatar" height="46" width="46"/>
        </td>
        <td>{this.props.singleItem.Name}</td>
        <td>$ {parseFloat(this.props.singleItem.Price).toFixed(2)}</td>
        <td><button className="pet-delete btn btn-success" onClick={this.handleAddToCart}>
        <span className="glyphicon glyphicon-plus"></span></button></td>
      </tr>
    ) // return
  } // render
}); //DevList

module.exports=DevList;
