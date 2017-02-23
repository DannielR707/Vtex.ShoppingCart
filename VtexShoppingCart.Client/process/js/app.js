var React = require('react');
var ReactDOM = require('react-dom');
var _ = require('lodash');

var DevList = require('./DevList');
var SearchDevs = require('./SearchDevs');

var MainInterface = React.createClass({
  getInitialState: function() {
    return {
      aptBodyVisible: false,
      orderBy: 'Name',
      orderDir: 'asc',
      queryText: '',
      myDevelopers: []
    } //return
  }, //getInitialState

  componentDidMount: function() {

  }, //componentDidMount

  componentWillUnmount: function() {
    this.serverRequest.abort();
  }, //componentWillUnmount

  deleteMessage: function(item) {
    var allApts = this.state.myDevelopers;
    var newApts = _.without(allApts, item);
    this.setState({
      myDevelopers: newApts
    }); //setState
  }, //deleteMessage

  toggleAddDisplay: function() {
    var tempVisibility = !this.state.aptBodyVisible;
    this.setState({
      aptBodyVisible: tempVisibility
    }); //setState
  }, //toggleAddDisplay

  addItem: function(tempItem) {
    var tempApts = this.state.myDevelopers;
    tempApts.push(tempItem);
    this.setState({
      myDevelopers: tempApts
    }); //setState
  }, //addItem

  reOrder: function(orderBy, orderDir) {
    this.setState({
      orderBy: orderBy,
      orderDir: orderDir
    }); //setState
  }, //reOrder

  searchDevelopers(q) {
    this.serverRequest = $.get('http://130.211.212.145/api/developer/GetDevelopersByOrganization?organizationName=' + q, function(result) {
      var tempDevs = result;
      this.setState({
        myDevelopers: tempDevs
      }); //setState
    }.bind(this));
  }, //searchDevelopers

  render: function() {
    var filteredApts = [];
    var orderBy = this.state.orderBy;
    var orderDir = this.state.orderDir;
    var queryText = this.state.queryText;
    var myDevelopers = this.state.myDevelopers;

    filteredApts = myDevelopers.map(function(item, index) {
      return(
        <DevList key = { index }
          singleItem = { item }
          whichItem = { item } />
      ) //return
    }.bind(this)); //filteredApts.map
    return (
      <div className="row">
        <SearchDevs
          onSearch = { this.searchDevelopers }
        />
        <h2>Add developer</h2>
        <table className="table">
          <thead>
              <tr>
                <th>Picture</th>
                <th>Username</th>
                <th>Price</th>
                <th></th>
              </tr>
          </thead>
          <tbody>
            {filteredApts}
          </tbody>
        </table>
      </div>
    ) //return
  } //render
}); //MainInterface

ReactDOM.render(
  <MainInterface />,
  document.getElementById('developersList')
); //render
