import { useState, useEffect } from "react";
import { ProSidebar, Menu, MenuItem, SubMenu } from "react-pro-sidebar";
import { Box, Typography, useTheme } from "@mui/material";
import { Link } from "react-router-dom";
import "react-pro-sidebar/dist/css/styles.css";
import { tokens } from "../theme";
import { SidebarDataAdmin } from './SideBar';
import MenuOutlinedIcon from "@mui/icons-material/MenuOutlined";
import { useSelector } from "react-redux";
import axios from "../setups/custom_axios"
const Item = ({ title, to, icon }) => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  return (
    <MenuItem
      style={{
        color: colors.grey[100],
      }}
      icon={icon}
    >
      <Typography style={{ fontSize: 15 }}>{title}</Typography>
      <Link to={to} />
    </MenuItem>
  );
};
const Sidebar = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [isCollapsed, setIsCollapsed] = useState(false);
  const { user } = useSelector((state) => state.auth);
  const [dataNhanKhau, setDataNhanKhau] = useState([]);
  const [SidebarData, setSidebarData] = useState();
  useEffect(() => {
    if (user) {
      setSidebarData(SidebarDataAdmin);
    }
  }, [user])
  useEffect(
    async () => {
      try {
        const response = await axios.get(`/nhan-khau/danh-sach-nhan-khau-chua-co-ho-khau`)
        const datas = response.data;
        const datamap = datas.map((data) => {
          const label = data.hoTen + " " + data.maNhanKhau
          return {
            label,
            value: data.maNhanKhau
          }
        })
        setDataNhanKhau(datamap);
      } catch (error) {
        console.log(error);
      }
    }, []
  )
  return (
    <div>
      <Box
        sx={{
          "& .pro-sidebar-inner": {
            background: `${colors.primary[400]} !important`,
          },
          "& .pro-icon-wrapper": {
            backgroundColor: "transparent !important",
          },
          "& .pro-inner-item": {
            padding: "5px 25px 5px 20px !important",
          },
          "& .pro-inner-item:hover": {
            color: "#868dfb !important",
          },
          "& .pro-menu-item.active": {
            color: "#6870fa !important",
          },
          minHeight: '609px'
        }}
      >
        <ProSidebar collapsed={isCollapsed}  >
          <Menu iconShape="square">
            <MenuItem
              onClick={() => setIsCollapsed(!isCollapsed)}
              icon={isCollapsed ? <MenuOutlinedIcon /> : undefined}
              style={{
                margin: "10px 0 10px 0",
                color: colors.grey[100],
              }}
            >
              {!isCollapsed && (
                <Box
                  style={{ textAlign: 'center' }}
                >
                  <Typography variant="h3" color={colors.grey[100]} style={{ fontSize: 22 }}>
                    QUẢN LÝ CHUNG CƯ
                  </Typography>
                </Box>
              )}
            </MenuItem>
            {!isCollapsed && (
              <Box mb="25px">
                <Box display="flex" justifyContent="center" alignItems="center">
                  <img
                    alt="profile-user"
                    width="100px"
                    height="100px"
                    src={`../../avatar.jpg`}
                    style={{ borderRadius: "50%" }}
                  />
                </Box>
                <Box textAlign="center">
                  <Typography
                    variant="h2"
                    color={colors.grey[100]}
                    fontWeight="bold"
                    sx={{ m: "10px 0 0 0", fontSize: 22 }}
                  >
                    {user?.firstName + " " + user?.lastName}
                  </Typography>
                  <Typography variant="h5" color={colors.greenAccent[500]}>
                    Ban quản trị
                  </Typography>
                </Box>
              </Box>
            )}
            <Box paddingLeft={isCollapsed ? undefined : "10%"} style={{ color: 'black' }}>
              {SidebarData?.map((item, index) => {
                if (item.subNav) {
                  return (
                    <SubMenu
                      key={index}
                      title={item.title}
                      icon={item.icon}>
                      {item.subNav.map((itm, indx) => {
                        if (!(itm.path === 'household-add')) {
                          return (
                            <Item key={indx}
                              to={itm.path}
                              icon={itm.icon}
                              title={itm.title}></Item>
                          );
                        } else {
                          return (
                            <Item key={indx}
                              to={{
                                pathname: itm.path,
                                state: dataNhanKhau
                              }}
                              data={itm.data}
                              icon={itm.icon}
                              title={itm.title}
                            ></Item>
                          )
                        }
                      })}
                    </SubMenu>
                  );
                } else {
                  if (!item.data) {
                    return (
                      <Item key={index}
                        to={item.path}
                        icon={item.icon}
                        title={item.title}></Item>
                    );
                  } else {
                    return (
                      <Item key={index}
                        to={item.path}
                        icon={item.icon}
                        title={item.title}
                        data={item.data}
                      ></Item>
                    )
                  }
                }
              })}
            </Box>
          </Menu>
        </ProSidebar>
      </Box>
    </div>
  );
};
export default Sidebar;
