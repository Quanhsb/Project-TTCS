import { Route, Switch } from "react-router-dom";
import LoginPage from "../pages/Login";
const UnAuth = (props) => {
  return (
    <>
      <Switch>
        <>
          <Route path="*" exact component={LoginPage} />
        </>
      </Switch>
    </>
  );
};
export default UnAuth;
