import { Route, Switch } from "react-router-dom";
import TabernaclePage from "../pages/TabernaclePage";
const tabernacleRoute = (props) => {
  return (
    <>
      <Switch>
        <>
          <Route path="*" exact component={TabernaclePage} />
        </>
      </Switch>
    </>
  );
};
export default tabernacleRoute;
