import React from "react";
import ReactDOM from "react-dom";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";

import { App } from "./App";

import "./styles/index.scss";

ReactDOM.render(
  <React.StrictMode>
    <Router history={createBrowserHistory()}>
      <App />
    </Router>
  </React.StrictMode>,
  // eslint-disable-next-line comma-dangle
  document.getElementById("root"),
);
