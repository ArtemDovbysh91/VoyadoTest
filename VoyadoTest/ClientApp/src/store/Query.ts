import { Action, Reducer } from 'redux';
import {AppThunkAction} from "./index";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface QueryState {
    query: string;
    queryResult: SearchResult;
    isLoading: boolean;
}

export interface SearchResult {
    query: string;
    googleSearchResult: number;
    bingSearchResult: number;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

export interface UpdateSearchAction { type: 'UPDATE_SEARCH', query: string }
export interface RequestSearchAction { type: 'REQUEST_SEARCH', query: string }
export interface ReceiveSearchAction { type: 'RECEIVE_SEARCH', query: string, result: SearchResult }


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
export type KnownAction = UpdateSearchAction | RequestSearchAction | ReceiveSearchAction

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    updateSearch: (query: string) => ({ type: 'UPDATE_SEARCH', query: query } as UpdateSearchAction),    
    requestSearch: (query: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.search && query !== appState.search.queryResult.query) {
            fetch(`search?query=${query}`)
                .then(response => response.json() as Promise<SearchResult>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_SEARCH', query: query, result: data });
                });

            dispatch({ type: 'REQUEST_SEARCH', query: query });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unrequestedQuery: QueryState = 
    { 
        query: '', 
        queryResult: 
        {
            query: '', 
            bingSearchResult: 0, 
            googleSearchResult: 0
        }, 
        isLoading: false 
    };

export const reducer: Reducer<QueryState> = (state: QueryState | undefined, incomingAction: Action): QueryState => {
    if (state === undefined) {
        return unrequestedQuery;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'UPDATE_SEARCH':
            return {
                query: action.query,
                queryResult: state.queryResult,
                isLoading: false
            }
        case 'REQUEST_SEARCH':
            return {
                query: action.query,
                queryResult: state.queryResult,
                isLoading: true
            };
        case 'RECEIVE_SEARCH':
            return {
                query: action.query,
                queryResult: action.result,
                isLoading: false
            };
        default:
            return state;
    }
};
