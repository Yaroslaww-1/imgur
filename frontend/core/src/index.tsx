import React, { createContext } from "react";
import ReactDOM from "react-dom";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";

import { App } from "./App";

import AuthStore from "stores/auth.store";

import "./styles/index.scss";

interface IState {
  store: AuthStore;
}

export const authStore = new AuthStore();

export const Context = createContext<IState>({ store: authStore });

ReactDOM.render(
  <Context.Provider value={{ store: authStore }}>
    <React.StrictMode>
      <Router history={createBrowserHistory()}>
        <App />
      </Router>
    </React.StrictMode>
  </Context.Provider>,
  // eslint-disable-next-line comma-dangle
  document.getElementById("root"),
);
