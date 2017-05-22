using System.Web;
using System.Web.Optimization;

namespace WebAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                 "~/Assets/admin/libs/jquery/dist/jquery.js",
                 "~/Assets/admin/libs/moment/moment.js",
                 "~/Assets/admin/libs/moment/min/locales.js",
                 "~/Assets/admin/libs/moment/min/moment-with-locales.js",
                 "~/Assets/admin/libs/fastclick/lib/fastclick.js",
                 "~/Assets/admin/libs/slimScroll/jquery.slimscroll.js",
                 "~/Assets/admin/libs/eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker.js",
                 "~/Assets/admin/libs/angular/angular.js",
                 "~/Assets/admin/libs/angular-ui-router/release/angular-ui-router.js",
                 "~/Assets/admin/libs/angular-sanitize/angular-sanitize.js",
                "~/Assets/admin/libs/toastr/toastr.js",
                "~/Assets/admin/libs/bootbox/bootbox.js",
                "~/Assets/admin/libs/ngBootbox/ngBootbox.js",
                "~/Assets/admin/libs/ckeditor/ckeditor.js",
                "~/Assets/admin/libs/ckfinder/ckfinder.js",
                "~/Assets/admin/libs/ng-ckeditor/ng-ckeditor.js",
                "~/Assets/admin/libs/angular-local-storage/dist/angular-local-storage.js",
                "~/Assets/admin/libs/angular-loading-bar/src/loading-bar.js",
                "~/Assets/admin/libs/checklist-model/checklist-model.js",
                "~/Assets/admin/libs/ng-dialog/js/ngDialog.js",
                "~/Assets/admin/libs/angular-ui-select/dist/select.js",
                "~/Assets/admin/libs/ng-file-upload/ng-file-upload-shim.js",
                "~/Assets/admin/libs/ng-file-upload/ng-file-upload.js",
                "~/Assets/admin/libs/bootstrap/dist/js/bootstrap.js",
                "~/Assets/admin/libs/ui-bootstrap/ui-bootstrap.js",
                "~/Assets/admin/libs/ui-bootstrap/ui-bootstrap-tpls.js"
                ));


            bundles.Add(new ScriptBundle("~/js/mains").Include(
                "~/app/shared/modules/tk.common.js",
                "~/app/shared/directives/pagerDirective.js",
                "~/app/shared/directives/datetimeDirective.js" ,
                "~/app/shared/services/apiService.js" ,
                "~/app/shared/services/notificationService.js",
                "~/app/shared/services/commonService.js",
                "~/app/shared/filters/statusFilter.js",
                "~/app/shared/filters/reportStatusFilter.js",
                "~/app/shared/filters/datetimeFilter.js",
                "~/app/shared/services/authData.js" ,
                "~/app/shared/services/authenticationService.js",
                "~/app/shared/services/loginService.js",
                "~/app/components/situation_categories/situationCategories.module.js",
                "~/app/components/application_groups/applicationGroups.module.js",
                "~/app/components/application_roles/applicationRoles.module.js",
                "~/app/components/application_users/applicationUsers.module.js",
                "~/app/components/situations/situations.module.js",
                "~/app/components/statistic_categories/statisticCategories.module.js",
                "~/app/components/statistics/statistic.module.js",
                "~/app/components/daily_sheets/dailySheets.module.js",
                "~/app/app.js",
                "~/app/components/home/rootController.js",
                "~/app/components/home/homeController.js",
                "~/app/components/login/loginController.js",
                "~/app/components/application_users/applicationUserAddController.js",
                "~/app/components/application_users/applicationUserEditController.js",
                "~/app/components/application_users/applicationUserListController.js",
                "~/app/components/application_roles/applicationRoleAddController.js",
                "~/app/components/application_roles/applicationRoleEditController.js",
                "~/app/components/application_roles/applicationRoleListController.js",
                "~/app/components/application_groups/applicationGroupListController.js",                
                "~/app/components/application_groups/applicationGroupEditController.js",
                "~/app/components/application_groups/applicationGroupAddController.js",
                "~/app/components/situation_categories/situationCategoryAddController.js",
                "~/app/components/situation_categories/situationCategoryListController.js",
                "~/app/components/situation_categories/situationCategoryEditController.js",
                "~/app/components/situations/situationListController.js" ,
                "~/app/components/situations/situationAddController.js",
                "~/app/components/situations/situationEditController.js",
                "~/app/components/statistic_categories/statisticCategoryAddController.js",
                "~/app/components/statistic_categories/statisticCategoryEditController.js",
                "~/app/components/statistic_categories/statisticCategoryListController.js",
                "~/app/components/statistics/statisticAddController.js",
                "~/app/components/statistics/statisticEditController.js",
                "~/app/components/statistics/statisticListController.js" ,
                "~/app/components/daily_sheets/dailySheetAddController.js" ,
                "~/app/components/daily_sheets/dailySheetEditController.js",
                "~/app/components/daily_sheets/dailySheetListController.js",
                "~/app/components/daily_sheets/dailySheetStatisticController.js"
                ));
            BundleTable.EnableOptimizations = false;
            //bundles.Add(new StyleBundle("~/css/base")
            //    .Include("~/Assets/client/css/bootstrap.css", new CssRewriteUrlTransform())
            //    .Include("~/Assets/client/font-awesome-4.6.3/css/font-awesome.css", new CssRewriteUrlTransform())
            //    .Include("~/Assets/admin/libs/jquery-ui/themes/smoothness/jquery-ui.min.css", new CssRewriteUrlTransform())
            //    .Include("~/Assets/client/css/style.css", new CssRewriteUrlTransform())
            //    .Include("~/Assets/client/css/custom.css", new CssRewriteUrlTransform())
            //    );
            //BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.GetByKey("EnableBundles"));


        }
    }
}
