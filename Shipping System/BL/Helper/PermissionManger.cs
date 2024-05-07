namespace Shipping_System.BL.Helper
{
    public class PermissionManger
    {
        public List<Permission> getAllPermisssions()
        {
            List<Permission> permissions = new List<Permission>()
            {
               new Permission()
                {
                    Name = "اضافة موظف",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل موظف",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض الموظفين",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف موظف",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "اضافة تاجر",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل تاجر",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض التجار",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف تاجر",
                    isExtiedToTheRole =false,
                },
                 new Permission()
                {
                    Name = "اضافة مندوب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل مندوب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض المناديب",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف مندوب",
                    isExtiedToTheRole =false,
                },
                 new Permission()
                {
                    Name = "اضافة محافظة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل محافظة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض المحافظات",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف محافظة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "اضافة مدينة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل مدينة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض المدن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "حذف مدينة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "اضافة فرع",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل فرع",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض الافرع",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف فرع",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "اضافة صلاحية",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل صلاحية",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض الصلاحيات",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف صلاحية",
                    isExtiedToTheRole =false,
                },
                 new Permission()
                {
                    Name = "اضافة طلب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل طلب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل حالة طلب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض تفاصيل طلب",
                    isExtiedToTheRole =false,
                },
                 new Permission()
                {
                    Name = "عرض الطلبات",
                    isExtiedToTheRole =false,
                },
                  new Permission()
                {
                    Name = "عرض تقارير الطلبات",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "حذف طلب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "اضافة اعداد شحن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل اعداد شحن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض اعدادات الشحن",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف اعداد شحن",
                    isExtiedToTheRole =false,
                },
                 new Permission()
                {
                    Name = "تعديل اعداد الوزن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض اعداد الوزن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل اعداد القرية",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض اعداد القرية",
                    isExtiedToTheRole =false,
                },
            };

            return permissions;
        }
        public List<Permission> getEmployeesPermisssions()
        {
            List<Permission> employeesPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة موظف",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل موظف",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض الموظفين",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف موظف",
                    isExtiedToTheRole =false,
                },

            };

            return employeesPermissions;
        }
        public List<Permission> getTradersPermisssions()
        {
            List<Permission> tradersPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة تاجر",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل تاجر",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض التجار",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف تاجر",
                    isExtiedToTheRole =false,
                },

            };

            return tradersPermissions;
        }
        public List<Permission> getRepresentivesPermisssions()
        {
            List<Permission> representivesPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة مندوب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل مندوب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض المناديب",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف مندوب",
                    isExtiedToTheRole =false,
                },

            };

            return representivesPermissions;
        }
        public List<Permission> getGovernatesPermisssions()
        {
            List<Permission> governatesPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة محافظة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل محافظة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض المحافظات",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف محافظة",
                    isExtiedToTheRole =false,
                },

            };

            return governatesPermissions;
        }
        public List<Permission> getCitiesPermisssions()
        {
            List<Permission> citiesPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة مدينة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل مدينة",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض المدن",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف مدينة",
                    isExtiedToTheRole =false,
                },

            };

            return citiesPermissions;
        }
        public List<Permission> getBranchsPermisssions()
        {
            List<Permission> branchsPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة فرع",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل فرع",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض الافرع",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف فرع",
                    isExtiedToTheRole =false,
                },

            };

            return branchsPermissions;
        }
        public List<Permission> getRolesPermisssions()
        {
            List<Permission> rolesPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة صلاحية",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل صلاحية",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض الصلاحيات",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف صلاحية",
                    isExtiedToTheRole =false,
                },

            };

            return rolesPermissions;
        }
        public List<Permission> getOrdersPermisssions()
        {
            List<Permission> ordersPermissions = new List<Permission>()
            {
                 new Permission()
                {
                    Name = "اضافة طلب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل طلب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل حالة طلب",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض تفاصيل طلب",
                    isExtiedToTheRole =false,
                },
                 new Permission()
                {
                    Name = "عرض الطلبات",
                    isExtiedToTheRole =false,
                },
                  new Permission()
                {
                    Name = "عرض تقارير الطلبات",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "حذف طلب",
                    isExtiedToTheRole =false,
                },
            };
            return ordersPermissions;
        }
        public List<Permission> getShippingSettingsPermisssions()
        {
            List<Permission> shippingSettingsPermissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = "اضافة اعداد شحن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "تعديل اعداد شحن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض اعدادات الشحن",
                    isExtiedToTheRole =false,
                }, new Permission()
                {
                    Name = "حذف اعداد شحن",
                    isExtiedToTheRole =false,
                },

            };

            return shippingSettingsPermissions;
        }
        public List<Permission> getWeightSettingsPermisssions()
        {
            List<Permission> weightSettingsPermisssions = new List<Permission>()
            {

                new Permission()
                {
                    Name = "تعديل اعداد الوزن",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض اعداد الوزن",
                    isExtiedToTheRole =false,
                },

            };

            return weightSettingsPermisssions;
        }
        public List<Permission> getVillageSettingsPermisssions()
        {
            List<Permission> villageSettingsPermisssions = new List<Permission>()
            {

                new Permission()
                {
                    Name = "تعديل اعداد القرية",
                    isExtiedToTheRole =false,
                },
                new Permission()
                {
                    Name = "عرض اعداد القرية",
                    isExtiedToTheRole =false,
                },

            };

            return villageSettingsPermisssions;
        }
    }
}
