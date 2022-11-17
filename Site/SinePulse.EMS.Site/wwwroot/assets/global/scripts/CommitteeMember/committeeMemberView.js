function renderCommitteeMemberPersonalInfo() {
  $("#committeeMemberPersonalInfo").load(
    "/CommitteeMember/LoadCommitteeMemberPersonalInfoPartialView?committeeMemberId=" + $('#committeeMemberId').val(),
    function () {

    });
}

function renderCommitteeMemberAddress() {
  $("#committeeMemberAddress").load(
    "/CommitteeMember/LoadCommitteeMemberAddressPartialView?committeeMemberId=" + $('#committeeMemberId').val(),
    function () {

    });
}