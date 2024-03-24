import React from 'react';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import * as BsIcons from "react-icons/bs";
export const SidebarDataAdmin = [
  {
    title: 'Trang chủ',
    path: "/",
    icon: <AiIcons.AiFillHome />
  },
  {
    title: 'Quản lý hộ gia đình',
    icon: <FaIcons.FaAddressBook />,
    subNav: [
      {
        title: 'Hộ khẩu',
        path: '/household',
        icon: <FaIcons.FaListAlt />
      },
      {
        title: 'Căn hộ',
        path: '/room',
        icon: <BsIcons.BsHouseDoorFill />,
      }
    ]
  },
  {
    title: 'Quản lý nhân dân',
    icon: <FaIcons.FaAddressBook />,
    subNav: [
      {
        title: 'Nhân khẩu',
        path: '/demographic',
        icon: <BsIcons.BsPersonCheckFill />
      },
      {
        title: 'Tạm trú',
        path: '/tabernacle',
        icon: <BsIcons.BsPersonPlusFill />,
      },
      {
        title: 'Tạm vắng',
        path: '/absent',
        icon: <BsIcons.BsPersonDashFill />,
      }
    ]
  },
  {
    title: 'Quản lý khoản thu',
    path: '/revenue',
    icon: <AiIcons.AiFillMoneyCollect />
  }
];
