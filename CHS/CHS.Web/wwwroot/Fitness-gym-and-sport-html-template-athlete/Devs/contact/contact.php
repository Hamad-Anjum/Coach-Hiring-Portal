<?php 
/***
FUNCTION SEND MAIL
***/
require_once('config.php');

$fromName = isset($_REQUEST['name'])? $_REQUEST['name']:'';
$fromEmail = isset($_REQUEST['email'])? $_REQUEST['email']:'';
$message = isset($_REQUEST['message'])? $_REQUEST['message']:'';

if($fromName && checkEmail($fromEmail) && $message){
	$array1 = array('[FROM-NAME]','[FROM-EMAIL]','[MESSAGE]');
	$array2 = array($fromName, $fromEmail, $message);
	
	$mailSubject = str_replace($array1, $array2, $mailSubject);
	$mailMessage = str_replace($array1, $array2, $mailMessage);	
	
	$result = sendMailUTF8($mailTo, $fromName, $fromEmail, $mailSubject, $mailMessage);
	$feedback = array();
	$feedback['status'] = $result;
	if($result){
		$feedback['message'] = $sentMessage;
	}else{
		$feedback['message'] = $failMessage;
	}
}else{
	$feedback['message'] = $invalidData;
}
echo json_encode($feedback);
exit;

function sendMailUTF8($to, $from_user, $from_email, $subject = '(No subject)', $message = '')
{
	$from_user = "=?UTF-8?B?".base64_encode($from_user)."?=";
	$subject = "=?UTF-8?B?".base64_encode($subject)."?=";
	$headers = "From: $from_user <$from_email>\r\n".
		   "MIME-Version: 1.0" . "\r\n" .
		   "Content-type: text/html; charset=UTF-8" . "\r\n";
	return @mail($to, $subject, $message, $headers);
}	
function checkEmail($email){
	if(!filter_var($email, FILTER_VALIDATE_EMAIL))
	{
		return false;
	}
	else
	{
		return true;
	}
}