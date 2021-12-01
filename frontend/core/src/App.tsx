import { Route, Switch } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import { observer } from "mobx-react-lite";

import { AppRoute } from "@common/enums/app-route.enum";

import { Context } from "index";

import { CreatePost } from "pages/create-post";
import { Login } from "pages/login";
import { PrivateRoute } from "@components/private-route";

export const App = observer(() => {
  const { store } = useContext(Context);
  const [authenticating, setAuthenticating] = useState<boolean>(true);

  useEffect(() => {
    (async () => {
      setAuthenticating(true);
      if (localStorage.getItem("accessToken")) {
        await store.checkAuth();
        setAuthenticating(false);
      }
    })();
  }, []);

  return (
    !authenticating
      ? <Switch>
        <PrivateRoute path={AppRoute.CREATE_POST} component={CreatePost} />
        <Route path={AppRoute.LOGIN} component={Login} />
      </Switch>
      : <div>loading</div>
  );
});
