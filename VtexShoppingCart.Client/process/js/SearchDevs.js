var React = require('react');

var SearchDevs = React.createClass({

  handleSearch: function(e) {
    this.props.onSearch(this.refs.inputOrganizationName.value);
  }, //handleSearch

  render: function() {
    return(
      <div className="row">
        <div className="col-sm-offset-3 col-sm-6">
          <div className="input-group">
            <input id="organizationFieldID" placeholder="Organization" ref="inputOrganizationName"
            type="text" className="form-control" aria-label="Search Developers" />
            <div className="input-group-btn">
              <button  type="button" className="btn btn-primary" onClick={ this.handleSearch }>
                Load Developers
              </button>
            </div>
          </div>
        </div>
      </div>
    ) // return
  } // render
}); //SearchAppointments

module.exports = SearchDevs;
