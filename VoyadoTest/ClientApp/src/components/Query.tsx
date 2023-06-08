import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { ApplicationState } from '../store';
import * as QueryStore from '../store/Query';

type QueryProps =
    QueryStore.QueryState &
    typeof QueryStore.actionCreators &
    RouteComponentProps<{}>;

class Query extends React.PureComponent<QueryProps> {
    private handleKeyDown(event: React.KeyboardEvent<HTMLInputElement>) : void {
        if (event.key === 'Enter') {
            this.props.requestSearch(this.props.query);
        }
    }
    public render() {
        return (
            <React.Fragment>
                <h1>Query</h1>

                <p>This is a simple words ranking service.</p>
                <input type="text" placeholder="name..." 
                       value={this.props.query} 
                       onChange={(e) => this.props.updateSearch(e.target.value)}
                       onKeyPress={(e) => this.handleKeyDown(e)}
                />
                
                <br/>
                <br/>
                
                <button type="button"
                    className="btn btn-primary btn-lg"
                    disabled={this.props.isLoading}
                    onClick={() => { this.props.requestSearch(this.props.query); }}>
                    Search
                    
                    {this.props.isLoading &&
                        <div className="spinner-grow text-warning" role="status">
                            <span className="sr-only">Loading...</span>
                        </div>
                    }
                </button>
                <br/>
                <br/>

                <p>Google result:  {this.props.queryResult.googleSearchResult.toLocaleString()} </p>
                <p>Bing result: {this.props.queryResult.bingSearchResult.toLocaleString()}  </p>
            </React.Fragment>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.search,
    QueryStore.actionCreators
)(Query as any);
