import { Route, Redirect, RouteProps } from "react-router";
import { observer } from "mobx-react-lite";

import { store } from "index";

import { AppRoute } from "@common/enums/app-route.enum";

export const PrivateRoute: React.FC<RouteProps> = observer(props => {
  return store.isAuth ? (
    <Route path={props.path} exact={props.exact} component={props.component} />
  ) : (
    <Redirect to={AppRoute.LOGIN} />
  );
});
