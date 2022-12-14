// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using System.Web.Mvc;
using $rootnamespace$;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof (NakedObjectsActivator), "PreStart")]
[assembly: PostApplicationStartMethod(typeof (NakedObjectsActivator), "PostStart")]

namespace $rootnamespace$ {
    public static class NakedObjectsActivator {
        public static void PreStart() {
            //  Add any custom 'PreStart' behaviour here
        }

        public static void PostStart() {
            // Without this any value type fields with a default value will be set to mandatory by the MS unobtrusive validation
            // - that overrides the required NOF behaviour based on the 'Optionally' attribute.
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            //  Add any custom 'PostStart' behaviour here
        }
    }
}