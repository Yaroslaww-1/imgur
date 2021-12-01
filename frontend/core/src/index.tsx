import React, { createContext } from "react";
import ReactDOM from "react-dom";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";

import { App } from "./App";

import Store from "store/store";

import "./styles/index.scss";

interface IState {
  store: Store;
}

export const store = new Store();

export const Context = createContext<IState>({ store });

ReactDOM.render(
  <Context.Provider value={{ store }}>
    <React.StrictMode>
      <Router history={createBrowserHistory()}>
        <App />
      </Router>
    </React.StrictMode>
  </Context.Provider>,
  // eslint-disable-next-line comma-dangle
  document.getElementById("root"),
);
